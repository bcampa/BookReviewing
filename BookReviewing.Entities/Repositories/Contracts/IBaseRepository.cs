using BookReviewing.Shared.Filters;
using System.Collections.Generic;

namespace BookReviewing.Entities.Repositories.Contracts
{
    public interface IBaseRepository<TEntity>
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteById(int id);
        void DeleteRange(IEnumerable<TEntity> entities);
        TEntity GetById(int id);
        List<TEntity> GetMany(PaginationFilter filter);
        void SaveChanges();
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
    }
}
