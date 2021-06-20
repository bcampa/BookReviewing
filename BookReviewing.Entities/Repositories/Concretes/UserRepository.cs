using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Contracts;
using System;
using System.Linq;

namespace BookReviewing.Entities.Repositories.Concretes
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository() : base() { }

        public User GetByGuid(Guid guid)
        {
            return _context.Users
                .Where(x => x.Guid == guid)
                .SingleOrDefault();
        }
    }
}
