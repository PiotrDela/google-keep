using GoogleKeep.Domain.Entities;
using GoogleKeep.Infrastructure.Notes;
using GoogleKeep.Tests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleKeep.IntegrationTests.SeedWork
{
    public class NotesWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly INoteRepository repository = new NoteInMemoryRepository();
        private Guid authenticatedUserId = Guid.Parse("3d7ceb77-5c90-42f4-95f3-65b9f7129bc4");

        public NotesWebApplicationFactory WithNote(Note note)
        {
            repository.AddAsync(note).GetAwaiter().GetResult();
            return this;
        }

        public NotesWebApplicationFactory WithAuthenticatedUser(Guid userId)
        {
            authenticatedUserId = userId;
            return this;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseTestServer().ConfigureTestServices(services =>
            {
                services.AddSingleton(repository);
                services.AddScoped(x => new FakeUserContext(authenticatedUserId));

                services.Configure<AuthenticationOptions>(x =>
                {
                    x.DefaultAuthenticateScheme = FakeAuthenticationHandler.AuthenticationSchema;
                });

                services.AddAuthentication(FakeAuthenticationHandler.AuthenticationSchema).AddScheme<JwtBearerOptions, FakeAuthenticationHandler>(FakeAuthenticationHandler.AuthenticationSchema, options => { });
            });
        }
    }
}
