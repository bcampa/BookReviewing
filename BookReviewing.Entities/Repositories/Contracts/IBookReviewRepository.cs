using BookReviewing.Entities.Models;
using BookReviewing.Shared.Filters;
using System.Collections.Generic;

namespace BookReviewing.Entities.Repositories.Contracts
{
    public interface IBookReviewRepository : IBaseRepository<BookReview>
    {
        List<BookReview> GetByFilter(BookReviewFilter filter);
    }
}
