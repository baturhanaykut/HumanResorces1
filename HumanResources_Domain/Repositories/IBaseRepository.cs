using HumanResources_Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Domain.Repositories
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        Task<bool> Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task<int> Save();

        Task<bool> Any(Expression<Func<T, bool>> expression);

        Task<T> GetDefault(Expression<Func<T, bool>> expression);

        
        Task<ICollection<T>> GetDefaults(Expression<Func<T, bool>> expression);

        Task<TResult> GetFiltredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include= null
            );

        Task<ICollection<TResult>> GetFilteredList<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            );
    }
}
