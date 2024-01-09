using GoogleKeep.Domain.Entities;
using GoogleKeep.Infrastructure.Notes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleKeep.Tests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            builder.UseEnvironment("Development");

            builder.ConfigureServices(services =>
            {
                services.AddSingleton<INoteRepository, NoteInMemoryRepository>();
            });

            base.ConfigureWebHost(builder);
        }
    }
}
