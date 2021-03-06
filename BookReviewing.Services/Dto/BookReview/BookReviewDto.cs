using BookReviewing.Services.Dto.User;
using System;

namespace BookReviewing.Services.Dto.BookReview
{
    public class BookReviewDto
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime LastUpdate { get; set; }
        public UserDto User { get; set; }
    }
}
