using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Contracts;
using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Messages.Book;

namespace BookReviewing.Services.DomainServices.Concretes
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void AddBook(BookCreatedMessage message)
        {
            var entity = new Book
            {
                Id = message.BookId
            };

            _bookRepository.Add(entity);
            _bookRepository.SaveChanges();
        }

        public void RemoveBook(BookRemovedMessage message)
        {
            _bookRepository.DeleteById(message.BookId);
            _bookRepository.SaveChanges();
        }
    }
}
