using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Messages.Book;

namespace BookReviewing.Api.Consumers
{
    public class BookCreatedConsumer : RabbitMqListener<BookCreatedMessage>
    {
        private readonly IBookService _service;

        public BookCreatedConsumer(IBookService service) : base("book-created")
        {
            _service = service;
        }

        protected override void HandleMessage(BookCreatedMessage message)
        {
            _service.AddBook(message);
        }
    }
}
