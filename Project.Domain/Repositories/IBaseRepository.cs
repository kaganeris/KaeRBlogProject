using Microsoft.EntityFrameworkCore.Query;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Repositories
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity); 
        Task<bool> Any(Expression<Func<T, bool>> exp); 
        Task<List<T>> GetAll();
        Task<T> GetDefault(Expression<Func<T, bool>> exp); 
        Task<List<T>> GetDefaults(Expression<Func<T, bool>> exp);

        // Tek sonuç dönecek
        Task<TResult> GetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> select, // select işlemi
            Expression<Func<T, bool>> where, // Where işlemi
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, // sırlama işlemi
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null // Join işlemi
            );
        Task<List<TResult>> GetFilteredList<TResult>(
            Expression<Func<T, TResult>> select, // select işlemi
            Expression<Func<T, bool>> where, // Where işlemi
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, // sıralama işlemi
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null // Join işlemi
            );
    }
}
