using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Messages.Book;
using Microsoft.Extensions.DependencyInjection;

namespace BookReviewing.Api.Consumers
{
    public class BookRemovedConsumer : RabbitMqListener<BookRemovedMessage>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BookRemovedConsumer(IServiceScopeFactory serviceScopeFactory) : base("book-removed")
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override void HandleMessage(BookRemovedMessage message)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IBookService>();
                service.RemoveBook(message);
            }
        }
    }
}
