using Reborn.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Reborn.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("email", user.email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration["JWT:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(configuration["JWT:Issuer"],
                configuration["JWT:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(double.Parse(configuration["JWT:LifetimeInHours"])),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
