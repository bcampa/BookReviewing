using BookReviewing.Entities;
using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

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
            _bookRepository.Add(new Book());
            _bookRepository.SaveChanges();
            _userRepository.Add(new User());
            _userRepository.SaveChanges();
            var bookReview = new BookReview { UserId = 1, BookId = 1 };
            _bookReviewRepository.Add(bookReview);

            Assert.DoesNotThrow(() => _bookReviewRepository.SaveChanges());
        }
    }
}