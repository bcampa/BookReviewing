using BookReviewing.Services.Messages.User;

namespace BookReviewing.Services.DomainServices.Contracts
{
    public interface IUserService
    {
        void AddUser(UserCreatedMessage message);
    }
}
