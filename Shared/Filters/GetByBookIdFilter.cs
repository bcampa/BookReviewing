namespace BookReviewing.Shared.Filters
{
    public class GetByBookIdFilter : PaginationFilter
    {
        public int BookId { get; set; }
    }
}
