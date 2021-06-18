using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Messages.User;

namespace BookReviewing.Api.Consumers
{
    public class UserCreatedConsumer : RabbitMqListener<UserCreatedMessage>
    {
        private readonly IUserService _service;

        public UserCreatedConsumer(IUserService service) : base("user-created")
        {
            _service = service;
        }

        protected override void HandleMessage(UserCreatedMessage message)
        {
            _service.AddUser(message);
        }
    }
}
