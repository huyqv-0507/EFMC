using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EFMC.Service.Common.Constants;
using EFMC.Service.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EFMC.Service.Common.Utils
{
    public class JwtUtils
    {

        public string GenerateJwtToken(UserModel userModel, IConfiguration configuration)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtClaimConstant.USER_ID, userModel.UserId.ToString()),
            new Claim(JwtClaimConstant.USER_NAME, userModel.UserName.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[JwtConfConstant.KEY]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration[JwtConfConstant.EXPIRE_DAYS]));

            var token = new JwtSecurityToken(
                issuer: configuration[JwtConfConstant.ISSUER],
                audience: configuration[JwtConfConstant.AUDIENCE],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
