using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        IEnumerable<TEntity> GetAll();

        Task AddAsync(TEntity entity);

        void Remove(TEntity entity);

        void Remove(int id);

        void Update(TEntity entity);

        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
    }
}