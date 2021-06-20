using System;
using System.Collections.Generic;

namespace BookReviewing.Entities.Models
{
    public class User
    {
        public User()
        {
            BookReview = new HashSet<BookReview>();
        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookReview> BookReview { get; set; }
    }
}
