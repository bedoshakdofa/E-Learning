using E_Learning.Data.Model;
using E_Learning.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace E_Learning.services
{
    public class TokenServices:ITokenServices
    {
        private readonly SymmetricSecurityKey _key;
        public TokenServices(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTOKEN:secretKey"]));
        }

        public string GetToken(User user)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.SSN.ToString()),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var cred=new SigningCredentials(_key,SecurityAlgorithms.HmacSha256Signature);

            var TokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                SigningCredentials = cred,
                Expires = DateTime.Now.AddDays(7)

            };

            var TokenHandler=new JwtSecurityTokenHandler();
            var Token = TokenHandler.CreateToken(TokenDesciptor);
            return TokenHandler.WriteToken(Token);
        }
    }
}
