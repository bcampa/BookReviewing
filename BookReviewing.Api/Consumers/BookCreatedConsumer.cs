using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Messages.Book;
using Microsoft.Extensions.DependencyInjection;

namespace BookReviewing.Api.Consumers
{
    public class BookCreatedConsumer : RabbitMqListener<BookCreatedMessage>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BookCreatedConsumer(IServiceScopeFactory serviceScopeFactory) : base("book-created")
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override void HandleMessage(BookCreatedMessage message)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IBookService>();
                service.AddBook(message);
            }
        }
    }
}
