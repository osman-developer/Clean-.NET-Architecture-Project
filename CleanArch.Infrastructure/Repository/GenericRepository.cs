using CleanArch.Infrastructure;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly APIDbContext _context;

        public GenericRepository(APIDbContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
           var entity =  _context.Set<T>().Find(id);
           _context.Set<T>().Remove(entity);
           return _context.SaveChanges() > 0;
        }
        public bool Delete(string id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges() > 0;
        }


        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual Task<List<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual Task<List<T>> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = _context.Set<T>().Where(predicate);
            foreach (var includeExpression in includeExpressions)
            {
                set = DynamicInclude(set, includeExpression);
            }

            return set.ToListAsync();
        }

        public virtual Task<List<T>> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = _context.Set<T>();
            foreach (var includeExpression in includeExpressions)
            {
                set = DynamicInclude(set, includeExpression);
            }

            return set.ToListAsync();
        }

        private IQueryable<T> DynamicInclude(IQueryable<T> set, Expression<Func<T, object>> IncludeExpression)
        {
            if (IncludeExpression.Body.NodeType == ExpressionType.Call)
            {
                ParameterInfo[] parameters;
                var includeInfo = typeof(EntityFrameworkQueryableExtensions).GetMethods().Where(info => info.Name == "Include" &&
                (parameters = info.GetParameters()).Length == 2 && typeof(Expression).IsAssignableFrom(parameters[1].ParameterType)).Single();

                var thenIncludeInfo = typeof(EntityFrameworkQueryableExtensions).GetMethods().Where(info => info.Name == "ThenInclude").ToList()[1];
                var LastThenIncludeInfo = typeof(EntityFrameworkQueryableExtensions).GetMethods().Where(info => info.Name == "ThenInclude").ToList()[0];

                var lambda = IncludeExpression as LambdaExpression;
                var method = IncludeExpression.Body as MethodCallExpression;
                var result = new List<Expression>();
                while (method != null)
                {
                    result.Add(Expression.Lambda(method.Arguments[0], lambda.Parameters[0]));
                    lambda = method.Arguments[1] as LambdaExpression;
                    method = lambda.Body as MethodCallExpression;
                }
                result.Add(lambda);
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < result.Count; i++)
                {
                    var navProps = result[i].ToString().Split('.');
                    if (navProps.Length > 2)
                    {
                        for (int j = 0; j < navProps.Length; j++)
                        {
                            if (j == 0)
                            {
                                continue;
                            }
                            stringBuilder.AppendFormat("{0}{1}", navProps[i], j == navProps.Length - 1 && i == result.Count - 1 ? string.Empty : ".");
                        }
                    }
                    else
                    {
                        stringBuilder.AppendFormat("{0}{1}", navProps[i], i == result.Count - 1 ? string.Empty : ".");
                    }
                }

                set = set.Include(stringBuilder.ToString());
            }
            else
            {
                set = set.Include(IncludeExpression);
            }
            return set;
        }

        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges() > 0;
        }
        public T AddWithReturn(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return  entity;
        }

        public T UpdateWithReturn(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }

        public bool AddRange(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return _context.SaveChanges() > 0;
        }

         public List<T> AddRangeWithReturn(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public bool UpdateRange(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteRange(IQueryable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return _context.SaveChanges() > 0;
        }
        public bool DeleteRange(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return _context.SaveChanges() > 0;
        }
    }
}
