using Azure.Data.Tables;
using GoogleKeep.Domain.Entities;
using GoogleKeep.Infrastructure.AzureStorage;
using GoogleKeep.Infrastructure.Notes;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

public partial class Program { }