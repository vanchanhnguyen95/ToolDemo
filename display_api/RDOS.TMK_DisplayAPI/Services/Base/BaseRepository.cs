using Microsoft.EntityFrameworkCore;
using Npgsql;
using RDOS.TMK_DisplayAPI.Infrastructure;
using Sys.Common.Helper;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RDOS.TMK_DisplayAPI.Services.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
          where TEntity : class
    {
        protected readonly ApplicationDbContext _dataContext;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
            this.dbSet = _dataContext.Set<TEntity>();
        }

        public bool Contains(Expression<Func<TEntity, bool>> precidate)
        {
            return _dataContext.Set<TEntity>().Count(precidate) > 0;
        }

        public virtual TEntity Delete(object id)
        {
            var entity = _dataContext.Set<TEntity>().Find(id);
            if (entity == null)
                return entity;
            _dataContext.Set<TEntity>().Remove(entity);
            _dataContext.SaveChanges();
            return entity;
        }

        public virtual TEntity GetById(object id)
        {
            var entity = _dataContext.Set<TEntity>().Find(id);
            return entity;
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dataContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dataContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }          

            return query;
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dataContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var query = this._dataContext.Set<TEntity>();
            return query;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            TEntity thisEntity = _dataContext.Set<TEntity>().Add(entity).Entity;
            if (_dataContext.SaveChanges() > 0)
            {
                return thisEntity;
            }

            return null;
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dataContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.SingleOrDefault();
        }

        public virtual TEntity Update(TEntity entity)
        {
            var local = _dataContext.Set<TEntity>().Local.FirstOrDefault();
            if (local != null)
                _dataContext.Entry(local).State = EntityState.Detached;

            _dataContext.Entry(entity).State = EntityState.Modified;
            if (_dataContext.SaveChanges() > 0)
            {
                return entity;
            }
            return null;
        }

        public IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query);
            }
            return query;
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
            _dataContext.SaveChanges();
        }

        public void Save()
        {
            try
            {
                _dataContext.SaveChanges();
            }
            catch (NpgsqlException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                if (!(ex.InnerException?.Message ?? ex.Message).Contains("Cannot insert duplicate key"))
                    throw;
            }
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            foreach (var entity in entities)
            {
                _dataContext.Entry(entity).State = EntityState.Modified;
            }
            _dataContext.SaveChanges();
        }

        public void DeleteByInt(int id)
        {
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            _dataContext.SaveChanges();
        }
    }
}
