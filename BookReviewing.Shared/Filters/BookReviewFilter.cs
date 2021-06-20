using System;

namespace BookReviewing.Shared.Filters
{
    public class BookReviewFilter : PaginationFilter
    {
        public int? BookId { get; set; }
        public Guid? UserGuid { get; set; }
    }
}
