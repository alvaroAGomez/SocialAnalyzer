using SocialAnalyzer.DAL.Models;
using SocialAnalyzer.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario = SocialAnalyzer.Services.Models.Usuario;

namespace SocialAnalyzer.Services.Interfaces
{
    public interface IUsuarioService 
    {
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario> SaveUsuarioAsync(Usuario usuario);
        Task<IList<Usuario>> GetUsuariosByFilter(string userNameFilter);
        Task<IList<Usuario>> GetUserforRol(string rol);
        Task<bool> BajaUsuarioAsync(int id);
        Task<bool> verifUserName(string userNameFilter);
        Task<IList<Usuario>> GetAllAsync();
        Task<bool> ChangePasswordAsync(int id, string oldPass, string newPass);
    }
}
