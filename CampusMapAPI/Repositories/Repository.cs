using CampusMapAPI.Data;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CampusMapAPI.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Base
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return query.AnyAsync();
        }

        public virtual Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return query.CountAsync();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _dbSet.AddAsync(entity);

            return entity;
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);

            return true;
        }

        public virtual Task<bool> ExistsAsync(int id)
        {
            return _dbSet.AnyAsync(e => e.Id == id);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(bool asNoTracking = false)
        {
            IQueryable<T> query = _dbSet;

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id, bool asNoTracking = false)
        {
            IQueryable<T> query = _dbSet;

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        public virtual void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Remove(entity);
        }

        public virtual async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Update(entity);
        }
    }
}
