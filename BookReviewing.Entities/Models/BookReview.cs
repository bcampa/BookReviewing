namespace BookReviewing.Entities.Models
{
    public class BookReview
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
