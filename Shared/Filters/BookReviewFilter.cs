namespace BookReviewing.Shared.Filters
{
    public class BookReviewFilter : PaginationFilter
    {
        public int? BookId { get; set; }
        public int? UserId { get; set; }
    }
}
