using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Contexts;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Realizations
{
    class BaseEntity
    {
        public int id { get; set; }
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal protected LibraryContext context;
        internal protected DbSet<TEntity> dbSet;

        public Repository(LibraryContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Remove(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            TEntity entity = dbSet.Find(id);
            Remove(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return dbSet.FromSqlRaw(query, parameters).ToList();
        }
    }
}