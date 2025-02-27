using Common.Pagination;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Persistence.Concrete;
using Persistence.DBContext;
using Domain.Interfaces.Database;

namespace Infrastructure.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal HealthTriageDbContext _ctx;
        public GenericRepository(HealthTriageDbContext HealthTriageDbContext)
        {
            _ctx = HealthTriageDbContext;
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public async Task DeleteById(string id)
        {
            var entity = await _ctx.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return;
            }

            EntityEntry entityEntry = _ctx.Set<T>().Remove(entity);
        }

        public async Task SoftDelete(string id)
        {
            var entity = await _ctx.Set<T>().FindAsync(id);

            if (entity is HealthTriageEntity toedurEntity)
            {
                toedurEntity.Status = Status.Deleted;
                toedurEntity.DeletedDate = DateTime.UtcNow;
                _ctx.Set<T>().Update(entity);
            }
            else
            {
                throw new InvalidOperationException($"{typeof(T).Name} does not support SoftDelete.");
            }
        }

        public async Task<PaginatedList<T>> GetAll(int pageNumber, int pageSize, Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = filter == null
                                  ? _ctx.Set<T>()
                                  : _ctx.Set<T>().Where(filter);

            // For pagination
            var data = query.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .AsNoTracking();

            return await PaginatedList<T>.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = filter == null
                                     ? _ctx.Set<T>()
                                     : _ctx.Set<T>().Where(filter);

            var data = await query.AsNoTracking().ToListAsync();

            return data;
        }

        public virtual async Task Add(T entity)
        {
            EntityEntry entityEntry = await _ctx.Set<T>().AddAsync(entity);
        }


        public async Task Update(T entity)
        {
            EntityEntry entityEntry = _ctx.Set<T>().Update(entity);
            entityEntry.State = EntityState.Modified;
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            return await _ctx.Set<T>().SingleOrDefaultAsync(filter);
        }

        public async Task<T> GetById(string id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _ctx.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var keyProperty = _ctx.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.FirstOrDefault();
            var keyName = keyProperty?.Name;

            if (string.IsNullOrEmpty(keyName))
            {
                throw new InvalidOperationException("Primary key not found for entity type.");
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, keyName) == id);
        }

        public async Task Delete(T entity)
        {
            _ctx.Remove(entity);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _ctx.Set<T>().AddAsync(entity);
        }
        
    }
}
