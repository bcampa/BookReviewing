using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Messages.Book;

namespace BookReviewing.Api.Consumers
{
    public class BookRemovedConsumer : RabbitMqListener<BookRemovedMessage>
    {
        private readonly IBookService _service;

        public BookRemovedConsumer(IBookService service) : base("book-removed")
        {
            _service = service;
        }

        protected override void HandleMessage(BookRemovedMessage message)
        {
            _service.RemoveBook(message);
        }
    }
}
