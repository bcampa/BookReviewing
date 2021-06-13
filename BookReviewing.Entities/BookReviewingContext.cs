using BookReviewing.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReviewing.Entities
{
    public class BookReviewingContext : DbContext
    {
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<BookReview> BookReviews { get; set; }
        private static bool _created = false;

        public BookReviewingContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\BookReviewingDB\book_reviewing.db");
    }
}
