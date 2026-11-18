using Microsoft.IdentityModel.Tokens;
using school.Models;
using school.Services.Iservices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace school.Services
{
    public class JwtService(IConfiguration configuration) : IJwt
    {
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.GivenName,user.Fullname)
               
            };
            claims.AddRange(user.Roles.Select(rl=>new Claim(ClaimTypes.Role,rl.Name.ToString())));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptors = new JwtSecurityToken
            (
                issuer:configuration.GetValue<string>("Jwt:Issuer"),
                audience:configuration.GetValue<string>("Jwt:Audience"),
                claims:claims,
                expires:DateTime.UtcNow.AddHours(2),
                signingCredentials:creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptors);
        }
    }
}
