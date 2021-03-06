using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Contracts;
using BookReviewing.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookReviewing.Entities.Repositories.Concretes
{
    public class BookReviewRepository : BaseRepository<BookReview>, IBookReviewRepository
    {
        public BookReviewRepository() : base() { }

        public List<BookReview> GetByFilter(BookReviewFilter filter)
        {
            IQueryable<BookReview> query = _context.BookReviews
                .AsNoTracking()
                .Include(x => x.User);

            if (filter.BookId.HasValue)
                query = query.Where(x => x.BookId == filter.BookId);
            if (filter.UserGuid.HasValue)
                query = query.Where(x => x.User.Guid == filter.UserGuid);

            var paginatedQuery = query
                .Skip(filter.CurrentPage * filter.PageSize)
                .Take(filter.PageSize);

            return paginatedQuery.ToList();
        }
    }
}
