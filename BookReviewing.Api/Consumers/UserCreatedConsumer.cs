using System;

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

    public class UserCreatedMessage
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
