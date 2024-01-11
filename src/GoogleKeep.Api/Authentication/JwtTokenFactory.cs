using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoogleKeep.Api.Authentication
{
    public class JwtTokenFactory
    {
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ba9142eff388474c95811256682c2ea604a4902fa5c54e69a1d54d269f0ae3bd"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var issuer = "https://mysite.com";
            var audience = "https://mysite.com";

            return new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(2)),
                claims: basicClaims,
                signingCredentials: creds
            );
        }
    }
}
