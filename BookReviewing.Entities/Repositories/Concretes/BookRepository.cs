using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Contracts;

namespace BookReviewing.Entities.Repositories.Concretes
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository() : base() { }
    }
}
