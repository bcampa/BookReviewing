using System;

namespace BookReviewing.Services.Messages.User
{
    public class UserCreatedMessage
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
