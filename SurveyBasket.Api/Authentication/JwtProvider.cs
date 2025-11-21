
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SurveyBasket.Api.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        public (string token, int expiresIn) GenerateToken(ApplicationUser user)
        {
            Claim[] claims = [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                ];
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3d42bb71f735dd74"));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var expiresIn = 60; // minutes
            var expiration = DateTime.UtcNow.AddMinutes(expiresIn);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "SurveyBasket.Api",
                audience: "SurveyBasket.Api users",
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );
            return (new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken), expiresIn);
        }
    }
}
