using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace GoogleKeep.IntegrationTests.SeedWork
{
    public class FakeAuthenticationHandler : AuthenticationHandler<JwtBearerOptions>
    {
        public const string AuthenticationSchema = "FakeAuthenticationSchema";

        private readonly FakeUserContext fakeUserContext;

        public FakeAuthenticationHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, FakeUserContext fakeUserContext) : base(options, logger, encoder)
        {
            this.fakeUserContext = fakeUserContext;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var userId = fakeUserContext.UserId;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Test user"),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim("UserId", userId.ToString()),
            };

            var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, JwtBearerDefaults.AuthenticationScheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
