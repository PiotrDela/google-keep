using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GoogleKeep.Api.Authentication
{

    [ApiController]
    [Route("api/[controller]")]
    public partial class AuthenticationController : Controller
    {
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly JwtTokenFactory jwtTokenFactory;

        public AuthenticationController(IAuthenticationProvider authenticationProvider, JwtTokenFactory jwtTokenFactory)
        {
            this.authenticationProvider = authenticationProvider ?? throw new ArgumentNullException(nameof(authenticationProvider));
            this.jwtTokenFactory = jwtTokenFactory ?? throw new ArgumentNullException(nameof(jwtTokenFactory));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Authenticate()
        {
            var identityUser = this.authenticationProvider.AuthenticateUser();
            if (identityUser == null)
            {
                return Unauthorized();
            }

            var userId = identityUser.Id;
            var userName = identityUser.UserName;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim("UserId", userId.ToString())
            };

            var token = this.jwtTokenFactory.CreateToken(userName, claims);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = token.ValidTo
            });
        }
    }
}
