using BookReviewing.Services.Messages.Book;

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
}
