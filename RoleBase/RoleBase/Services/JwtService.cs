using Microsoft.IdentityModel.Tokens;
using RoleBase.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace RoleBase.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User user, string RoleName) 
        {

            var Claim = new List<Claim>
           {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role , RoleName ),
            new Claim(ClaimTypes.Name, user.Name)
           };
            var key = new SymmetricSecurityKey
                (
                 Encoding.UTF8.GetBytes(_configuration["Jwt:key"])
                );

            var credentials = new SigningCredentials
              (
               key,
               SecurityAlgorithms.HmacSha256
              );

            var token = new JwtSecurityToken
              (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: Claim,
                expires: DateTime.Now.AddHours(2),
                 signingCredentials: credentials
              );
            return new JwtSecurityTokenHandler()
                .WriteToken( token );

        }
            
    }
}
