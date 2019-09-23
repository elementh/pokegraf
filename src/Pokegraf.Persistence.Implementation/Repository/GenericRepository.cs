using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pokegraf.Persistence.Contract.Repository;

namespace Pokegraf.Persistence.Implementation.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal readonly DbContext Context;
        internal readonly DbSet<T> DbSet;

        public GenericRepository(DbContext dbContext)
        {
            Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = dbContext.Set<T>();
        }

        public async Task<T> FindById(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = DbSet.Where(predicate);

            result = includes.Aggregate(result, (current, includeExpression) => current.Include(includeExpression));

            return await result.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var result = DbSet.Where(i => true);

            result = includes.Aggregate(result, (current, includeExpression) => current.Include(includeExpression));

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<T>> SearchBy(Expression<Func<T, bool>> searchBy, params Expression<Func<T, object>>[] includes)
        {
            var result = DbSet.Where(searchBy);

            result = includes.Aggregate(result, (current, includeExpression) => current.Include(includeExpression));

            return await result.ToListAsync();
        }

        public virtual async Task<T> Insert(T entity)
        {
            var entry = await DbSet.AddAsync(entity);
            
            return entry.Entity;
        }

        public virtual T Update(T entityToUpdate)
        {
            var entry = DbSet.Attach(entityToUpdate);
            
            Context.Entry(entityToUpdate).State = EntityState.Modified;

            return entry.Entity;
        }

        public virtual T Delete(object id)
        {
            T entityToDelete = DbSet.Find(id);
            
            return Delete(entityToDelete);
        }

        public virtual T Delete(T entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            
            var entry = DbSet.Remove(entityToDelete);

            return entry.Entity;
        }
    }
}