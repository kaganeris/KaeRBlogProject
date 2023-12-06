using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.Repositories;
using Project.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Project.Domain.Enums;

namespace Project.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {

        private readonly AppDbContext context;
        protected DbSet<T> _table;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
            _table = this.context.Set<T>();
        }

        public async Task<bool> Any(Expression<Func<T, bool>> exp)
        {
            return await _table.AnyAsync(exp);
        }

        public async Task<bool> Create(T entity)
        {

            try
            {
                entity.CreatedDate = DateTime.Now;
                entity.Status = Status.Active;
                await _table.AddAsync(entity);
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                entity.DeletedDate = DateTime.Now;
                entity.Status = Status.Passive;
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;

            }

        }

        public async Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return await _table.FirstOrDefaultAsync(exp);
        }

        public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> exp)
        {
            return await _table.Where(exp).ToListAsync();
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _table; // SELECT * FROM Post gibi...

            if (where != null)
            {
                query = query.Where(where); // SELECT * FROM Post WHERE Status = 1 gibi..
            }

            if (include != null)
            {
                query = include(query); // JOIN işlemi
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var result = await query.Select(select).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }


            var result = await query.Select(select).ToListAsync();

            return result;
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                entity.UpdatedDate = DateTime.Now;
                entity.Status = Status.Modified;
                context.Entry<T>(entity).State = EntityState.Modified; // Güncelleme işlemini Entity State'ini değiştirerek yapıyoruz.
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
