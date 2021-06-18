using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Contracts;

namespace BookReviewing.Entities.Repositories.Concretes
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository() : base() { }
    }
}
