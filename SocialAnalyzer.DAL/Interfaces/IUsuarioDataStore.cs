using SocialAnalyzer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.Interfaces
{
    public interface IUsuarioDataStore : IDataStore<Usuario>
    {
        Task<Usuario> ValidateLoginAsync(string username, string password);

        Task<IList<Usuario>> GetAllAsync(Expression<Func<Usuario, bool>> condition);

        Task<Usuario> GetByIdAsync(int id);
    }
}
