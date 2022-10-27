using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SocialAnalyzer.DAL.Interfaces;
using SocialAnalyzer.SDK.Options;
using SocialAnalyzer.Services.Helpers.JWT;
using SocialAnalyzer.Services.Interfaces;
using SocialAnalyzer.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioDataStore _usuarioDataStore;
        private readonly ILoginUsuarioDataStore _loginUsuarioDataStore;
        private readonly JWTOptions _jwtOptions;
        private readonly AppOptions _appOptions;
        private readonly MessageOptions _msgOptions;
        private readonly IJWTHelper _jwtHelper;

        public AuthService(IOptions<JWTOptions> jwtOptions, IOptions<AppOptions> appOptions, IOptions<MessageOptions> msgOptions, IUsuarioDataStore usuarioDataStore, ILoginUsuarioDataStore loginUsuarioDataStore, IMapper mapper, IJWTHelper jwtHelper)
        {
            _mapper = mapper;
            _usuarioDataStore = usuarioDataStore;
            _loginUsuarioDataStore = loginUsuarioDataStore;
            _jwtOptions = jwtOptions.Value;
            _jwtHelper = jwtHelper;
            _appOptions = appOptions.Value;
            _msgOptions = msgOptions.Value;
        }

        public async Task<LoginResponse> LoginAsync(string userName, string password)
        {
            var userDAL = await _usuarioDataStore.ValidateLoginAsync(userName, password);
            var user = _mapper.Map<Usuario>(userDAL);
            var loginResponse = new LoginResponse();
            if (user != null)
            {
                loginResponse.Result = SignInResult.Success;
                loginResponse.Token = _jwtHelper.GenerateJSONWebToken(user);
                loginResponse.Nombre = user.Nombre;
                loginResponse.Apellido = user.Apellido;
                loginResponse.Id = user.IdUsuario;
                loginResponse.UserName= user.UserName;
                loginResponse.Rol = user.Rol.Nombre;
                loginResponse.tiempoExpire = _jwtOptions.ExpirationInMinutes;
                return loginResponse;
            }
            else
            {
                loginResponse.ResultMessage = "El usuario o contraseña ingresados son incorrectos.";
            }

            loginResponse.Result = SignInResult.Failed;

            return loginResponse;
        }

        public async Task<string> RefreshTokenAsync(int usuarioId)
        {
            
            var user = await _usuarioDataStore.GetByIdAsync(usuarioId);

            if (user != null)
                return await Task.FromResult(_jwtHelper.GenerateJSONWebToken(_mapper.Map<Usuario>(user)));
            else
                return null;
        }


    }
}
