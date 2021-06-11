using System.Collections.Generic;

namespace BookReviewing.Entities.Models
{
    public class Book
    {
        public Book()
        {
            BookReview = new HashSet<BookReview>();
        }

        public int Id { get; set; }

        public virtual ICollection<BookReview> BookReview { get; set; }
    }
}
