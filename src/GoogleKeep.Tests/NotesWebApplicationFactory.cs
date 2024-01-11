using GoogleKeep.Domain.Entities;
using GoogleKeep.Infrastructure.Notes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleKeep.Tests
{
    public class NotesWebApplicationFactory : WebApplicationFactory<Program>
    { 
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseTestServer().ConfigureTestServices(services =>
            {
                services.AddSingleton<INoteRepository, NoteInMemoryRepository>();
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(this.GetType().Assembly));

                services.Configure<AuthenticationOptions>(x => 
                { 
                    x.DefaultAuthenticateScheme = FakeAuthenticationHandler.AuthenticationSchema; 
                });

                services.AddAuthentication(FakeAuthenticationHandler.AuthenticationSchema).AddScheme<JwtBearerOptions, FakeAuthenticationHandler>(FakeAuthenticationHandler.AuthenticationSchema, options => { });
            });

            //builder.UseContentRoot(".");
            //builder.UseEnvironment("Development");

            //builder.ConfigureServices(services =>
            //{
            //    services.AddSingleton<INoteRepository, NoteInMemoryRepository>();
            //});

            //base.ConfigureWebHost(builder);
        }
    }
}
