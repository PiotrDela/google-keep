using Microsoft.AspNetCore.Identity;

namespace GoogleKeep.Api.Authentication
{
    public interface IAuthenticationProvider
    {
        public IdentityUser<Guid> AuthenticateUser();
    }
}
