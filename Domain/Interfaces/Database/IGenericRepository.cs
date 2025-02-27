using Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Database
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PaginatedList<T>> GetAll(int pageNumber, int pageSize, Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null);
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<T> GetById(string id, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(string id);
        Task Add(T entity);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task DeleteById(string id);
        Task Delete(T entity);
        Task SoftDelete(string id);
    }
}
