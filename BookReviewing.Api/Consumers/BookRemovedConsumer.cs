using BookReviewing.Services.Messages.Book;

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
}
