namespace BookReviewing.Services.Dto.BookReview
{
    public class UpdateBookReviewRequest
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public string Comment { get; set; }
    }
}
