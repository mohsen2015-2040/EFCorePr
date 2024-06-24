using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace EFCorePr.Services
{
    public class JWTTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JWTTokenGenerator(IConfiguration configuration) => _configuration = configuration;

        public string GenerateToken(string userName, DateTime dateTime)
        {
            var jwtSetting = _configuration.GetSection("JWTSettings");
            var key = new GenerateKey().Generate();


            var secretKey = Encoding.UTF8.GetBytes(jwtSetting["SecretKey"]);
            var audience = jwtSetting["Audience"];
            var issuer = jwtSetting["Issuer"];

            var signingCredential = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                Audience = audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                Issuer = issuer,
                Expires = dateTime,
                SigningCredentials = signingCredential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(securityToken);

        }
    }
}
