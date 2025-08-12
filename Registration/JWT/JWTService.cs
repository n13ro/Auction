using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Registration.JWT
{
    public class JWTService : IJWTService
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly SymmetricSecurityKey _key;
        private readonly int _exp;
        //еще поля

        public JWTService(IConfiguration cfg)
        {
            _issuer = cfg["Jwt:Issuer"] ?? "AuctionApi";
            _audience = cfg["Jwt:Audience"] ?? "AuctionClient";
            _key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(cfg["Jwt:Key"])
                );
            _exp = 60;
        }

        public string CreateToken(int userId, string email, string nickName)
        {
            var clms = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new(JwtRegisteredClaimNames.Email, email),
                new(JwtRegisteredClaimNames.Nickname, nickName),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: clms,
                expires: DateTime.UtcNow.AddMinutes(_exp),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public string ValidationToken(string token)
        //{ 
        //}
    }
}
