using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialAnalyzer.Services.Models;
using SocialAnalyzer.SDK.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.Services.Helpers.JWT
{
    public class JWTHelper : IJWTHelper
    {
        private readonly JWTOptions _jwtOptions;

        public JWTHelper(IOptions<JWTOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateJSONWebToken(Usuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



            //var claims = new List<Claim> {
            //    new Claim("Id", userInfo.Id.ToString()),
            //    new Claim("Username", userInfo.UserName.ToString()),
            //};

            //foreach (var funcion in userInfo.Funciones)
            //{
            //    claims.Add(new Claim("Funciones", funcion));
            //}

            //foreach (var actividad in userInfo.Actividades)
            //{
            //    claims.Add(new Claim("Actividades", actividad));
            //}

            //foreach (var atributo in userInfo.Atributos)
            //{
            //    claims.Add(new Claim("Atributos", atributo));
            //}

            var token = new JwtSecurityToken(_jwtOptions.Issuer,
              _jwtOptions.Issuer,
              // claims,
              expires: DateTime.Now.AddMinutes(_jwtOptions.ExpirationInMinutes),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
