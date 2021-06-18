using BookReviewing.Shared.Filters;
using System.Collections.Generic;

namespace BookReviewing.Services.Dto.Misc
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        //public int TotalPages { get; set; }
        //public int TotalEntries { get; set; }

        public PaginatedResponse(PaginationFilter filter, IEnumerable<T> data)
        {
            Data = data;
            CurrentPage = filter.CurrentPage;
            PageSize = filter.PageSize;
        }
    }
}
