using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Contracts;
using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Messages.User;

namespace BookReviewing.Services.DomainServices.Concretes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(UserCreatedMessage message)
        {
            var entity = new User
            {
                Id = message.UserId
            };

            _userRepository.Add(entity);
            _userRepository.SaveChanges();
        }
    }
}
