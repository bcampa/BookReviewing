using System;

namespace BookReviewing.Services.Dto.User
{
    public class CreateUserRequest
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
    }
}
