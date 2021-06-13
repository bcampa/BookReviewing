using BookReviewing.Entities;
using BookReviewing.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookReviewing.Entities.Repositories
{
    public class UserRepository
    {
        private readonly BookReviewingContext _context;

        public UserRepository()
        {
            _context = new BookReviewingContext();
        }

        public List<User> GetAll()
        {
            return _context.Users
                .AsNoTracking()
                .ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Add(User User)
        {
            _context.Users.Add(User);
        }

        public void Update(User User)
        {
            _context.Users.Update(User);
        }

        public void Delete(User User)
        {
            _context.Users.Remove(User);
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
