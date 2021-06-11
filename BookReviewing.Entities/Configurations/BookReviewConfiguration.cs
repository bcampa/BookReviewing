using BookReviewing.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReviewing.Entities.Configurations
{
    public class BookReviewConfiguration : IEntityTypeConfiguration<BookReview>
    {
        public void Configure(EntityTypeBuilder<BookReview> builder)
        {
            builder.ToTable("BOOK_REVIEW");

            builder.HasIndex(e => new { e.BookId, e.UserId })
                .HasDatabaseName("UQ_BOOK_REVIEW")
                .IsUnique();

            builder.HasIndex(e => e.BookId)
                .HasDatabaseName("IDX_FK_BOOK_REVIEW_BOOK")
                .IsUnique(false);

            builder.HasIndex(e => e.UserId)
                .HasDatabaseName("IDX_FK_BOOK_REVIEW_USER")
                .IsUnique(false);

            builder.Property(e => e.Score)
                .HasColumnName("COMMENT");

            builder.Property(e => e.Comment)
                .HasColumnName("COMMENT");

            builder.Property(e => e.DatePosted)
                .HasColumnName("DATE_POSTED")
                .HasColumnType("DATE")
                .HasDefaultValue("CURRENT_TIMESTAMP");

            builder.Property(e => e.LastUpdate)
                .HasColumnName("LAST_UPDATE")
                .HasColumnType("DATE")
                .HasDefaultValue("CURRENT_TIMESTAMP");

            builder.HasOne(e => e.Book)
                .WithMany(e => e.BookReview)
                .HasForeignKey(e => e.BookId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_BOOK_REVIEW_BOOK");

            builder.HasOne(e => e.User)
                .WithMany(e => e.BookReview)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_BOOK_REVIEW_USER");
        }
    }
}
