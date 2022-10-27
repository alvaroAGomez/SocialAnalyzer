using Microsoft.EntityFrameworkCore;
using SocialAnalyzer.DAL.Encryption;
using SocialAnalyzer.DAL.Interfaces;
using SocialAnalyzer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.DataStores
{
    public class UsuarioDataStore : DataStore<Usuario>, IUsuarioDataStore
    {
        private readonly analizerContext _dbContext;
        private EncryptionService _encrypt = new EncryptionService();

        public UsuarioDataStore(analizerContext context) : base(context)
        {
            _dbContext = context;
        }

        public new async Task<Usuario> InsertAndSaveAsync(Usuario entity)
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction()) { 

                try
                {

                if (await ExistsAsync(x => x.IdUsuario == entity.IdUsuario)) return null;
                var pass = entity.Password;


                var ePassword = _encrypt.Encrypt(entity.Password);
                entity.Password = ePassword;

                _dbContext.Add(entity);
                _dbContext.SaveChanges();

                _dbContext.Entry(entity).State = EntityState.Detached;
                    dbContextTransaction.Commit();
                    return entity;
            }
            catch (Exception e)
            {
                dbContextTransaction.Rollback();
                
                return null;
            }
        }
        }

        public async Task<Usuario> ValidateLoginAsync(string username, string password)
        {
            var ePassword = _encrypt.Encrypt(password);


            try
            {
                var user = await _dbContext.Usuarios
                .Include(x=>x.Rol)
                .Include(x=>x.Estado)
                .SingleOrDefaultAsync(x => x.Username == username || x.Email == username);

                if (user != null && user.Password == ePassword)
                {
                    return user;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public new async Task<IList<Usuario>> GetAllAsync(Expression<Func<Usuario, bool>> condition)
        {
            var user = await _dbContext.Usuarios
                .Include(x=>x.Estado)
                .Include(x => x.Rol)
                .Where(condition)
                .OrderBy(x => x.Username)
                .ToListAsync();

            return user;
        }

        public new async Task<Usuario> GetAsync(Expression<Func<Usuario, bool>> condition)
        {
            var user = await _dbContext.Usuarios
                            .Include(x => x.Rol)
                            .SingleOrDefaultAsync(condition);

            return user;
        }

        public new async Task<Usuario> GetByIdAsync(int id)
        {
            var user = await _dbContext.Usuarios
                .Include(x => x.Rol)
                .Include(x=>x.Estado)
                .FirstOrDefaultAsync(x => x.IdUsuario == id);


            return user;
        }
    }
}
