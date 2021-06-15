namespace BookReviewing.Services.Dto.BookReview
{
    public class CreateBookReviewRequest
    {
        public float Score { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
