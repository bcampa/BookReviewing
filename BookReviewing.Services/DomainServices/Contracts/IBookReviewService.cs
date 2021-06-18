using BookReviewing.Services.Dto.BookReview;
using BookReviewing.Shared.Filters;
using System.Collections.Generic;

namespace BookReviewing.Services.DomainServices.Contracts
{
    public interface IBookReviewService
    {
        BookReviewDto Add(CreateBookReviewRequest request);
        void Delete(int id);
        IEnumerable<BookReviewDto> GetByFilter(BookReviewFilter filter);
        BookReviewDto GetById(int id);
        BookReviewDto Update(UpdateBookReviewRequest request);
    }
}
