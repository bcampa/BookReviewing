using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Contracts;
using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Messages.User;
using System;

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
                Guid = Guid.Parse(message.UserId),
                Name = message.Name
            };

            _userRepository.Add(entity);
            _userRepository.SaveChanges();
        }
    }
}
