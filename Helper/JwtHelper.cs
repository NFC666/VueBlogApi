using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VueBlog.API.Helper
{
    public class JwtHelper
    {
        private const string SecretKey = "1ryVPIoTM+h6BgvwWeFpG/pvuCMrZ/GnkveWvRHa5Ac="; // 不要有 "!"
        private const int ExpireMinutes = 60; // 令牌有效时间

        public static string GenerateToken(string username,string role="User")
        {
            var claims = new[]
{
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // 唯一 ID
            new Claim(ClaimTypes.Role, role) 
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "SenBlog",
                audience: "SenBlog",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(ExpireMinutes),
                signingCredentials: creds
            );




            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
