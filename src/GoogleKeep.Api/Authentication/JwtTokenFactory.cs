using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoogleKeep.Api.Authentication
{
    public class JwtTokenFactory
    {
        private readonly JwtSecurityTokenSettings settings;

        public JwtTokenFactory(JwtSecurityTokenSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public JwtSecurityToken CreateToken(string userName, IEnumerable<Claim> additionalClaims = null)
        {
            var basicClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (additionalClaims != null)
            {
                basicClaims.AddRange(additionalClaims);
            }

            var key = new SymmetricSecurityKey(this.settings.SigningKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: this.settings.Issuer,
                audience: this.settings.Audience,
                expires: DateTime.UtcNow.Add(this.settings.Lifespan),
                claims: basicClaims,
                signingCredentials: creds
            );
        }
    }
}
