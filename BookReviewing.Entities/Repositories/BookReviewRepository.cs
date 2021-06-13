using BookReviewing.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookReviewing.Entities.Repositories
{
    public class BookReviewRepository
    {
        private readonly BookReviewingContext _context;

        public BookReviewRepository()
        {
            _context = new BookReviewingContext();
        }

        public List<BookReview> GetAll()
        {
            return _context.BookReviews
                .AsNoTracking()
                .ToList();
        }

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

        public BookReview GetById(int id)
        {
            return _context.BookReviews.Find(id);
        }

        public void Add(BookReview bookReview)
        {
            _context.BookReviews.Add(bookReview);
        }

        public void Update(BookReview bookReview)
        {
            _context.BookReviews.Update(bookReview);
        }

        public void Delete(BookReview bookReview)
        {
            _context.BookReviews.Remove(bookReview);
        }

        public void Delete(int id)
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
