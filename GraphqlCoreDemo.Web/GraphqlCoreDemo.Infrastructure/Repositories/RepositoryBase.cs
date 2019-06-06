using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GraphqlCoreDemo.Infrastructure.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity FindBy(Expression<Func<TEntity, bool>> expression, bool tracking = true, bool includeAllNavs = false);
        TEntity FindBy(Expression<Func<TEntity, bool>> expression, bool tracking = true, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, bool includeAllNavs = false);
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression, bool tracking = true, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression, bool tracking = true, bool includeAllNavs = false, params Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>[] orderBy);

        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, bool includeAllNavs = false, params Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>[] orderBy);

        Task<IEnumerable<TEntity>> GetAllAsync(bool includeAllNavs = false, params Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>[] orderBy);
        Task<int> CountAsync();
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

    }

    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public virtual TEntity FindBy(Expression<Func<TEntity, bool>> expression, bool tracking = true, bool includeAllNavs = false)
        {
            IQueryable<TEntity> result = tracking ? _entities.Where(expression) : _entities.Where(expression).AsNoTracking();

            if (includeAllNavs)
            {
                foreach (var property in _context.Model.FindEntityType(typeof(TEntity)).GetNavigations())
                    result = result.Include(property.Name);
            }

            return result.FirstOrDefault();
        }
        public virtual TEntity FindBy(Expression<Func<TEntity, bool>> expression, bool tracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = tracking ? _entities.Where(expression) : _entities.Where(expression).AsNoTracking();

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return result.FirstOrDefault();
        }

        public virtual async Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, bool includeAllNavs = false)
        {
            IQueryable<TEntity> result = tracking ? _entities.Where(expression) : _entities.Where(expression).AsNoTracking();

            if (includeAllNavs)
            {
                foreach (var property in _context.Model.FindEntityType(typeof(TEntity)).GetNavigations())
                    result = result.Include(property.Name);
            }

            return await result.FirstOrDefaultAsync();
        }
        public virtual async Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = tracking ? _entities.Where(expression) : _entities.Where(expression).AsNoTracking();

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.FirstOrDefaultAsync();
        }

        public virtual IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression, bool tracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> result = tracking ? _entities.Where(expression) : _entities.Where(expression).AsNoTracking();

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return result.ToList();
        }
        public virtual IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression, bool tracking = true, bool includeAllNavs = false, params Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>[] orderBy)
        {
            IQueryable<TEntity> result = tracking ? _entities.Where(expression) : _entities.Where(expression).AsNoTracking();

            if (includeAllNavs)
            {
                foreach (var property in _context.Model.FindEntityType(typeof(TEntity)).GetNavigations())
                    result = result.Include(property.Name);
            }

            foreach (var orderByExpression in orderBy)
                result = orderByExpression(result);

            return result.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> result = tracking ? _entities.Where(expression) : _entities.Where(expression).AsNoTracking();

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.ToListAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, bool includeAllNavs = false, params Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>[] orderBy)
        {
            IQueryable<TEntity> result = tracking ? _entities.Where(expression) : _entities.Where(expression).AsNoTracking();

            if (includeAllNavs)
            {
                foreach (var property in _context.Model.FindEntityType(typeof(TEntity)).GetNavigations())
                    result = result.Include(property.Name);
            }

            foreach (var orderByExpression in orderBy)
                result = orderByExpression(result);

            return await result.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool includeAllNavs = false, params Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>[] orderBy)
        {
            IQueryable<TEntity> result = _entities.AsNoTracking();

            if (includeAllNavs)
            {
                foreach (var property in _context.Model.FindEntityType(typeof(TEntity)).GetNavigations())
                    result = result.Include(property.Name);
            }

            foreach (var orderByExpression in orderBy)
                result = orderByExpression(result);

            return await result.ToListAsync();
        }

        public virtual async Task<int> CountAsync()
        {
            return await _entities.CountAsync();
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");

            _entities.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            _entities.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");

            _entities.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            _entities.Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entity");

            _entities.RemoveRange(entities);
        }
    }

}
