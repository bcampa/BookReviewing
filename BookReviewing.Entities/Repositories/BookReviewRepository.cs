using BookReviewing.Entities.Models;
using BookReviewing.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookReviewing.Entities.Repositories
{
    public class BookReviewRepository : BaseRepository<BookReview>
    {
        public BookReviewRepository() : base() { }

        public List<BookReview> GetByFilter(BookReviewFilter filter)
        {
            IQueryable<BookReview> query = _context.BookReviews.AsNoTracking();

            if (filter.BookId.HasValue)
                query = query.Where(x => x.BookId == filter.BookId);
            if (filter.UserId.HasValue)
                query = query.Where(x => x.UserId == filter.UserId);

            var paginatedQuery = query
                .Skip(filter.CurrentPage * filter.PageSize)
                .Take(filter.PageSize);

            return paginatedQuery.ToList();
        }
    }
}
