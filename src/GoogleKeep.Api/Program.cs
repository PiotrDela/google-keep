using Azure.Data.Tables;
using GoogleKeep.Api.Authentication;
using GoogleKeep.Domain.Entities;
using GoogleKeep.Infrastructure.AzureStorage;
using GoogleKeep.Infrastructure.Notes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddHttpContextAccessor();

var azureStorageConnectionString = builder.Configuration.GetConnectionString("AzureStorage");
builder.Services.AddScoped(x => new TableServiceClient(azureStorageConnectionString));
builder.Services.AddScoped(x => TableNamingConvention.Default());
builder.Services.AddScoped<INoteRepository, AzureTableStorageRepository>();

var tokenSettings = new JwtSecurityTokenSettings("https://mysite.com", "https://mysite.com", Encoding.UTF8.GetBytes("ba9142eff388474c95811256682c2ea604a4902fa5c54e69a1d54d269f0ae3bd"), TimeSpan.FromHours(1));
builder.Services.AddScoped<IAuthenticationProvider, SimpleAuthenticationProvider>();
builder.Services.AddScoped(x => new JwtTokenFactory(tokenSettings));

// Configure Authentication
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = tokenSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = tokenSettings.Audience,
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(tokenSettings.SigningKey)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

public partial class Program { }