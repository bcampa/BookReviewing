using BookReviewing.Services.Messages.User;

namespace BookReviewing.Api.Consumers
{
    public class UserCreatedConsumer : RabbitMqListener<UserCreatedMessage>
    {
        public UserCreatedConsumer() : base("user-created") { }

        protected override void HandleMessage(UserCreatedMessage message)
        {
            // Do nothing
        }
    }
}
