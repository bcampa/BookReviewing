using System;

namespace BookReviewing.Services.Messages.Book
{
    public class BookCreatedMessage
    {
        public int BookId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
