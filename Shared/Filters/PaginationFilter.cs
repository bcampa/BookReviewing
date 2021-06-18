namespace BookReviewing.Shared.Filters
{
    public class PaginationFilter
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public PaginationFilter()
        {
            CurrentPage = 0;
            PageSize = 20;
        }
    }
}
