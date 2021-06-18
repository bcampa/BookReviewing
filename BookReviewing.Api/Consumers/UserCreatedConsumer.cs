using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Messages.User;
using Microsoft.Extensions.DependencyInjection;

namespace BookReviewing.Api.Consumers
{
    public class UserCreatedConsumer : RabbitMqListener<UserCreatedMessage>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserCreatedConsumer(IServiceScopeFactory serviceScopeFactory) : base("user-created")
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override void HandleMessage(UserCreatedMessage message)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IUserService>();
                service.AddUser(message);
            }
        }
    }
}
