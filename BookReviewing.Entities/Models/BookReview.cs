using System;

namespace BookReviewing.Entities.Models
{
    public class BookReview
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
