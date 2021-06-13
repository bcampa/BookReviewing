using BookReviewing.Entities;
using BookReviewing.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookReviewing.Entities.Repositories
{
    public class BookRepository
    {
        private readonly BookReviewingContext _context;

        public BookRepository()
        {
            _context = new BookReviewingContext();
        }

        public List<Book> GetAll()
        {
            return _context.Books
                .AsNoTracking()
                .ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Add(Book Book)
        {
            _context.Books.Add(Book);
        }

        public void Update(Book Book)
        {
            _context.Books.Update(Book);
        }

        public void Delete(Book Book)
        {
            _context.Books.Remove(Book);
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
