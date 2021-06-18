using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BookReviewing.Tests.RepositoryTests
{
    [TestFixture]
    public class BookReviewRepositoryTests
    {
        private BookRepository _bookRepository { get; set; }
        private UserRepository _userRepository { get; set; }
        private BookReviewRepository _bookReviewRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            _bookRepository = new BookRepository();
            _userRepository = new UserRepository();
            _bookReviewRepository = new BookReviewRepository();
        }

        [Test]
        public void AddWithoutValidForeignKeysShouldFail()
        {
            var bookReview = new BookReview();
            _bookReviewRepository.Add(bookReview);

            Assert.That(() => _bookReviewRepository.SaveChanges(),
                Throws.InstanceOf<DbUpdateException>());
        }

        [Test]
        public void AddShouldSucceed()
        {
            var book = new Book();
            _bookRepository.Add(book);
            _bookRepository.SaveChanges();
            var user = new User();
            _userRepository.Add(user);
            _userRepository.SaveChanges();
            var bookReview = new BookReview
            {
                UserId = user.Id,
                BookId = book.Id,
                Comment = "This review was created by a unit test."
            };
            _bookReviewRepository.Add(bookReview);

            Assert.DoesNotThrow(() => _bookReviewRepository.SaveChanges());
        }
    }
}