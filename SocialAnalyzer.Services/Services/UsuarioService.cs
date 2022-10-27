using AutoMapper;
using Microsoft.Extensions.Options;
using SocialAnalyzer.DAL.Encryption;
using SocialAnalyzer.DAL.Interfaces;
using SocialAnalyzer.Services.Interfaces;
using SocialAnalyzer.Services.Models;
using SocialAnalyzer.SDK.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialAnalyzer.DAL.DataStores;
using Microsoft.EntityFrameworkCore;

namespace SocialAnalyzer.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioDataStore _usuarioDataStore;
        private readonly IMapper _mapper;
        private readonly MessageOptions _msgOptions;
        private EncryptionService _encryptionService = new EncryptionService();

        public UsuarioService(IUsuarioDataStore usuarioDataStore, IMapper mapper, IOptions<MessageOptions> msgOptions)
        {
            _usuarioDataStore = usuarioDataStore;
            _mapper = mapper;
            _msgOptions = msgOptions.Value;


        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            try
            {
                if (id > 0)
                {
                    var usuario = await _usuarioDataStore.GetByIdAsync(id);
                    return _mapper.Map<Usuario>(usuario);
                }
                return null;
            }
            catch (Exception e)
            {
                var user = new Usuario();
                user.ResultMessage = _msgOptions.errorException + e.Message;
                throw;
            }
      
           
        }

        public async Task<IList<Usuario>> GetUsuariosByFilter(string userNameFilter)
        {
            if (!string.IsNullOrEmpty(userNameFilter))
            {
                var usuarios = await _usuarioDataStore.GetAllAsync(x => x.Username.Contains(userNameFilter));

                return _mapper.Map<IList<Usuario>>(usuarios);
            }
            else
            {
                var usuarios = await _usuarioDataStore.GetAllAsync(x => x.FechaBaja == null);
                return _mapper.Map<IList<Usuario>>(usuarios);
            }
        }

        public async Task<bool> verifUserName(string userNameFilter)
        {
            var usuarios = await _usuarioDataStore.GetAllAsync(x => x.Username.Equals(userNameFilter));
            var result = true;
            if (usuarios.Count > 0)
            {
                foreach (var user in usuarios)
                {
                    if (user.FechaBaja == null)
                    {
                        result = false;
                        return result;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            else
            {
                if (usuarios.Count == 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public async Task<IList<Usuario>> GetUserforRol(string rol)
        {
            if (!string.IsNullOrEmpty(rol))
            {
                var usuarios = await _usuarioDataStore.GetAllAsync(x => x.Rol.Nombre.Equals(rol));

                return _mapper.Map<IList<Usuario>>(usuarios);
            }
            else
            {

                return null;
            }
        }


        public async Task<Usuario> SaveUsuarioAsync(Usuario usuario)
        {
            try
            {
                
                    var usuarioDAL = _mapper.Map<DAL.Models.Usuario>(usuario);
                if (usuario.IdUsuario > 0)
                {
                    if (await _usuarioDataStore.ExistsAsync(x => x.FechaBaja == null && (x.Username == usuario.UserName || x.Email == usuario.Email) && x.IdUsuario != usuario.IdUsuario))
                    {
                        usuario.ResultMessage = _msgOptions.usernameExist;
                        return usuario;
                    }
                    else
                    {
         
                        var  excludedValues = new string[] { nameof(usuario.IdUsuario), nameof(usuario.Password) };
                     
                        var updatedUsr = await _usuarioDataStore.UpdateAndSaveAsync(x => x.IdUsuario == usuario.IdUsuario, usuarioDAL, excludedValues);
                        if (updatedUsr == null)
                        {
                            var u = new Usuario();
                            u.ResultMessage = _msgOptions.errorUpdate;
                            return u;
                        }

                        return _mapper.Map<Usuario>(updatedUsr);
                    }
                }
                else
                {
                    if (await _usuarioDataStore.ExistsAsync(x => x.FechaBaja == null && (x.Username == usuario.UserName || x.Email == usuario.Email)))
                    {
                        usuario.ResultMessage = _msgOptions.usernameExist;
                        return usuario;
                    }
                    else
                    {
                        //nuevo usuario
                       
                        var insertedUsr = await _usuarioDataStore.InsertAndSaveAsync(usuarioDAL);
                        if (insertedUsr == null) {
                            var u = new Usuario();
                            u.ResultMessage = _msgOptions.errorSave;
                            return u;
                        }

                        var user = await GetUsuarioByIdAsync(insertedUsr.IdUsuario);
                      
                        return user;
                    }

                }


            }
            catch (Exception e)
            {
                usuario.ResultMessage = _msgOptions.errorException + e.Message;
                return usuario;
            }
        }


        public async Task<bool> BajaUsuarioAsync(int id)
        {
            var usuarioBaja = await _usuarioDataStore.GetByIdAsync(id);

            usuarioBaja.FechaBaja = DateTime.Now;

            var excludedValues = new string[] { nameof(usuarioBaja.IdUsuario), nameof(usuarioBaja.Password), nameof(usuarioBaja.Nombre), nameof(usuarioBaja.Apellido), nameof(usuarioBaja.Email), nameof(usuarioBaja.Username) };

            var updatedUsr = await _usuarioDataStore.UpdateAndSaveAsync(x => x.IdUsuario == id, usuarioBaja, excludedValues);

            return updatedUsr != null ? true : false;
        }

        public async Task<IList<Usuario>> GetAllAsync()
        {
            var usuarios = await _usuarioDataStore.GetAllAsync(x => x.FechaBaja == null);
            return _mapper.Map<IList<Usuario>>(usuarios);
        }

        public async Task<bool> ChangePasswordAsync(int id, string oldPass, string newPass)
        {
            var usuarioChange = await _usuarioDataStore.GetByIdAsync(id);


            oldPass = _encryptionService.Encrypt(oldPass);

            if (oldPass != usuarioChange.Password) return false;

            newPass = _encryptionService.Encrypt(newPass);

            usuarioChange.Password = newPass;

            var excludedValues = new string[] { nameof(usuarioChange.IdUsuario), nameof(usuarioChange.FechaBaja), nameof(usuarioChange.Nombre), nameof(usuarioChange.Apellido), nameof(usuarioChange.Email), nameof(usuarioChange.Username) };

            var updatedUsr = await _usuarioDataStore.UpdateAndSaveAsync(x => x.IdUsuario == id, usuarioChange, excludedValues);

            return updatedUsr != null ? true : false;
        }
    }
}

