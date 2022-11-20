using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RDOS.TMK_DisplayAPI.Services.Base
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllQueryable(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);

        T SingleOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

        T FirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);

        IEnumerable<T> Find(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params Expression<Func<T, object>>[] includes);

        IEnumerable<T> Find(Expression<Func<T, bool>> filter);
        T Insert(T entity);
        void InsertRange(IEnumerable<T> entities);

        T Update(T entity);
        public void UpdateRange(IEnumerable<T> entities);

        T Delete(object id);
        public void DeleteRange(IEnumerable<T> entities);

        T GetById(object id);

        bool Contains(Expression<Func<T, bool>> precidate);
        void Save();
    }
}
