using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T,bool>> predicate);
        Task<List<T>> GetAll(Expression<Func<T,bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);
        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includeExpressions);
        Task<T> Get(int id);
        bool Update(T entity);
        bool Add(T entity);
        T AddWithReturn(T entity);
        T UpdateWithReturn(T entity);
        bool Delete(int id);
        bool Delete(string id);
        IQueryable<T> GetQueryable();

        bool AddRange(List<T> entities);
        List<T> AddRangeWithReturn(List<T> entities);
        bool UpdateRange(List<T> entities);
        bool DeleteRange(IQueryable<T> entities);
        bool DeleteRange(List<T> entities);
    }
}
