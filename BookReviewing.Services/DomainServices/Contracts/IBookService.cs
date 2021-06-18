using BookReviewing.Services.Messages.Book;

namespace BookReviewing.Services.DomainServices.Contracts
{
    public interface IBookService
    {
        void AddBook(BookCreatedMessage message);
        void RemoveBook(BookRemovedMessage message);
    }
}
