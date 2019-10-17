using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pokegraf.Persistence.Contract.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> FindById(object id);
        Task<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> SearchBy(Expression<Func<T, bool>> searchBy, params Expression<Func<T, object>>[] includes);
        Task<T> Insert(T entity);
        T Update(T entityToUpdate);
        T Delete(object id);
        T Delete(T entityToDelete);
    }
}