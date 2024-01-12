using Microsoft.AspNetCore.Identity;

namespace GoogleKeep.Api.Authentication
{
    public class SimpleAuthenticationProvider : IAuthenticationProvider
    {
        public IdentityUser<Guid> AuthenticateUser()
        {
            return new IdentityUser<Guid>("Fake user");
        }
    }
}
