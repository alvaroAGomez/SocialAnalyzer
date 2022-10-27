using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.Interfaces
{
    public interface IDataStore<T>
    {
        Task<bool> ExistsAsync(Expression<Func<T, bool>> condition);

        Task<T> GetAsync(Expression<Func<T, bool>> condition);

        //Task<T> GetByIdAsync(int id);

        Task<IList<T>> GetAllAsync();

        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> condition);

        Task<T> InsertAndSaveAsync(T entity);

        Task<T> UpdateAndSaveAsync(Expression<Func<T, bool>> condition, T entity, string[] excludedProperties);


    }
}
