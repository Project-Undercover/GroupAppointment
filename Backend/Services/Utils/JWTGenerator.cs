using Core.IUtils;
using Infrastructure.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils
{
    public class JWTGenerator : ITokenGenerator
    {
        public (string token, DateTime expireAt) GenerateToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constants.jwtSecret); //to be changed
            DateTime expireDate = DateTime.Now.AddDays(Constants.jwtExpirationDays);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = expireDate, //to be changed
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), expireDate);
        }


        public string ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenData = tokenHandler.ReadJwtToken(token);
            var result = tokenData.Payload;

            var key = Encoding.ASCII.GetBytes(Constants.jwtSecret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = (jwtToken.Claims.First(x => x.Type.ToLower() == "id").Value);

            return userId;
        }
    }
}
