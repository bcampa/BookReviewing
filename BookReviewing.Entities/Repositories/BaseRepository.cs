using BookReviewing.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookReviewing.Entities.Repositories
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly BookReviewingContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository()
        {
            _context = new BookReviewingContext();
            _dbSet = _context.Set<TEntity>();
        }

        public List<TEntity> GetMany(PaginationFilter filter)
        {
            return _dbSet
                .AsNoTracking()
                .Skip(filter.CurrentPage * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void DeleteById(int id)
        {
            var toBeDeleted = GetById(id);
            Delete(toBeDeleted);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
