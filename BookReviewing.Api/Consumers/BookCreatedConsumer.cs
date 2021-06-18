using System;

namespace BookReviewing.Api.Consumers
{
    public class BookCreatedConsumer : RabbitMqListener<BookCreatedMessage>
    {
        public BookCreatedConsumer() : base("book-created") { }

        protected override void HandleMessage(BookCreatedMessage message)
        {
            // Do nothing
        }
    }

    public class BookCreatedMessage
    {
        public int BookId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
