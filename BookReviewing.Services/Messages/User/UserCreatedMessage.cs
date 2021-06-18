using System;

namespace BookReviewing.Services.Messages.User
{
    public class UserCreatedMessage
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
