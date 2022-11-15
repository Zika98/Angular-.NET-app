using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {

        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        //KREIRANJE TOKENA PRILIKOM LOGINA
        public string CreateToken(AppUser user)
        {
            //ADDING CLAIMS
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Username)
            };

            //CREATING CREDENTIALS
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        

            //OPIS KAKO CE IZGLEDATI TOKEN
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
        
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //PRAVLJENJE TOKENA
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //DOBAVLJANJE TOKENA KOME TREBA
            return tokenHandler.WriteToken(token);

        }
    }
}