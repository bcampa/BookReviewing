using BookReviewing.Entities.Models;
using System;

namespace BookReviewing.Entities.Repositories.Contracts
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByGuid(Guid guid);
    }
}
