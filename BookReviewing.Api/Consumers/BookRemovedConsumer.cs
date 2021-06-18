using System;

namespace BookReviewing.Api.Consumers
{
    public class BookRemovedConsumer : RabbitMqListener<BookRemovedMessage>
    {
        public BookRemovedConsumer() : base("book-removed") { }

        protected override void HandleMessage(BookRemovedMessage message)
        {
            // Do nothing
        }
    }

    public class BookRemovedMessage
    {
        public int BookId { get; set; }
    }
}
