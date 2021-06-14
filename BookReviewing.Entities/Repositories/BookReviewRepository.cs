using BookReviewing.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookReviewing.Entities.Repositories
{
    public class BookReviewRepository : BaseRepository<BookReview>
    {
        public BookReviewRepository() : base() { }

        public List<BookReview> GetByBookId(int bookId)
        {
            return _context.BookReviews
                .AsNoTracking()
                .Where(x => x.BookId == bookId)
                .ToList();
        }

        public List<BookReview> GetByUserId(int userId)
        {
            return _context.BookReviews
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToList();
        }
    }
}
