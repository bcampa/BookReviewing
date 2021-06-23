using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Contracts;
using BookReviewing.Services.DomainServices.Concretes;
using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Dto.BookReview;
using BookReviewing.Shared.Filters;
using Moq;
using NUnit.Framework;
using System;

namespace BookReviewing.Tests.ServiceTests
{
    [TestFixture]
    internal class BookReviewServiceTests
    {
        private IBookReviewService _bookReviewService;
        private Mock<IBookReviewRepository> _bookReviewRepository;
        private Mock<IBookRepository> _bookRepository;
        private Mock<IUserRepository> _userRepository;

        [SetUp]
        public void SetUp()
        {
            _bookReviewRepository = new Mock<IBookReviewRepository>();
            _bookRepository = new Mock<IBookRepository>();
            _userRepository = new Mock<IUserRepository>();

            _bookReviewService = new BookReviewService(
                _bookReviewRepository.Object,
                _bookRepository.Object,
                _userRepository.Object);
        }

        [Test]
        public void GetByFilterShouldSucceed()
        {
            var filter = new BookReviewFilter();
            Assert.DoesNotThrow(() => _bookReviewService.GetByFilter(filter));
        }

        [Test]
        public void GetByIdShouldSucceed()
        {
            _bookReviewRepository
                .Setup(x => x.GetById(1))
                .Returns(new BookReview());

            Assert.DoesNotThrow(() => _bookReviewService.GetById(1));
        }

        [Test]
        public void GetByIdShouldFail()
        {
            Assert.That(() => _bookReviewService.GetById(1),
                Throws.InstanceOf<Exception>().With.Message.EqualTo("Book review not found"));
        }

        [Test]
        public void AddShouldSucceed()
        {
            _bookRepository
                .Setup(x => x.GetById(1))
                .Returns(new Book());
            _userRepository
                .Setup(x => x.GetByGuid(Guid.Parse("d3796c95-b344-4b2f-a029-5bc69849630d")))
                .Returns(new User());

            var creationRequest = new CreateBookReviewRequest
            {
                BookId = 1,
                UserGuid = "d3796c95-b344-4b2f-a029-5bc69849630d",
                Comment = "This review was created as a test.",
                Score = 5
            };
            Assert.DoesNotThrow(() => _bookReviewService.Add(creationRequest));
        }

        [Test]
        public void AddWithInvalidBookIdShouldFail()
        {
            _userRepository
                .Setup(x => x.GetByGuid(Guid.Parse("d3796c95-b344-4b2f-a029-5bc69849630d")))
                .Returns(new User());

            var creationRequest = new CreateBookReviewRequest
            {
                BookId = 1,
                UserGuid = "d3796c95-b344-4b2f-a029-5bc69849630d",
                Comment = "This review was created as a test.",
                Score = 5
            };
            Assert.That(() => _bookReviewService.Add(creationRequest),
                Throws.InstanceOf<Exception>().With.Message.EqualTo("Book not found"));
        }

        [Test]
        public void AddWithInvalidUserGuidShouldFail()
        {
            _bookRepository
                .Setup(x => x.GetById(1))
                .Returns(new Book());

            var creationRequest = new CreateBookReviewRequest
            {
                BookId = 1,
                UserGuid = "d3796c95-b344-4b2f-a029-5bc69849630d",
                Comment = "This review was created as a test.",
                Score = 5
            };
            Assert.That(() => _bookReviewService.Add(creationRequest),
                Throws.InstanceOf<Exception>().With.Message.EqualTo("User not found"));
        }

        [Test]
        public void AddWithScoreLowerThanZeroShouldFail()
        {
            _bookRepository
                .Setup(x => x.GetById(1))
                .Returns(new Book());
            _userRepository
                .Setup(x => x.GetByGuid(Guid.Parse("d3796c95-b344-4b2f-a029-5bc69849630d")))
                .Returns(new User());

            var creationRequest = new CreateBookReviewRequest
            {
                BookId = 1,
                UserGuid = "d3796c95-b344-4b2f-a029-5bc69849630d",
                Comment = "This review was created as a test.",
                Score = -0.1F
            };
            Assert.That(() => _bookReviewService.Add(creationRequest),
                Throws.InstanceOf<Exception>().With.Message.EqualTo("The score must be between 0 and 5"));
        }

        [Test]
        public void AddWithScoreGreaterThanFiveShouldFail()
        {
            _bookRepository
                .Setup(x => x.GetById(1))
                .Returns(new Book());
            _userRepository
                .Setup(x => x.GetByGuid(Guid.Parse("d3796c95-b344-4b2f-a029-5bc69849630d")))
                .Returns(new User());

            var creationRequest = new CreateBookReviewRequest
            {
                BookId = 1,
                UserGuid = "d3796c95-b344-4b2f-a029-5bc69849630d",
                Comment = "This review was created as a test.",
                Score = 5.1F
            };
            Assert.That(() => _bookReviewService.Add(creationRequest),
                Throws.InstanceOf<Exception>().With.Message.EqualTo("The score must be between 0 and 5"));
        }

        [Test]
        public void AddWithEmptyCommentShouldFail()
        {
            _bookRepository
                .Setup(x => x.GetById(1))
                .Returns(new Book());
            _userRepository
                .Setup(x => x.GetByGuid(Guid.Parse("d3796c95-b344-4b2f-a029-5bc69849630d")))
                .Returns(new User());

            var creationRequest = new CreateBookReviewRequest
            {
                BookId = 1,
                UserGuid = "d3796c95-b344-4b2f-a029-5bc69849630d",
                Comment = "",
                Score = 5
            };

            Assert.That(() => _bookReviewService.Add(creationRequest),
                Throws.InstanceOf<Exception>().With.Message.EqualTo("Comment must not be empty"));
        }

        [Test]
        public void UpdateShouldSucceed()
        {
            _bookReviewRepository
                .Setup(x => x.GetById(1))
                .Returns(new BookReview());

            var updateRequest = new UpdateBookReviewRequest
            {
                Id = 1,
                Comment = "This review was updated as a test.",
                Score = 5
            };

            Assert.DoesNotThrow(() => _bookReviewService.Update(updateRequest));
        }

        [Test]
        public void UpdateWithInvalidBookIdShouldFail()
        {
            var updateRequest = new UpdateBookReviewRequest
            {
                Id = 1,
                Comment = "This review was updated as a test.",
                Score = 5
            };

            Assert.That(() => _bookReviewService.Update(updateRequest),
                Throws.InstanceOf<Exception>().With.Message.EqualTo("Book review not found"));
        }

        [Test]
        public void UpdateWithEmptyCommentShouldFail()
        {
            _bookReviewRepository
                .Setup(x => x.GetById(1))
                .Returns(new BookReview());

            var updateRequest = new UpdateBookReviewRequest
            {
                Id = 1,
                Comment = "",
                Score = 5
            };

            Assert.That(() => _bookReviewService.Update(updateRequest),
                Throws.InstanceOf<Exception>().With.Message.EqualTo("Comment must not be empty"));
        }

        [Test]
        public void UpdateWithScoreLowerThanZeroShouldFail()
        {
            _bookReviewRepository
                .Setup(x => x.GetById(1))
                .Returns(new BookReview());

            var updateRequest = new UpdateBookReviewRequest
            {
                Id = 1,
                Comment = "",
                Score = -0.1F
            };

            Assert.That(() => _bookReviewService.Update(updateRequest),
                Throws.InstanceOf<Exception>().With.Message.EqualTo("The score must be between 0 and 5"));
        }

        [Test]
        public void UpdateWithScoreGreaterThanFiveShouldFail()
        {
            _bookReviewRepository
                .Setup(x => x.GetById(1))
                .Returns(new BookReview());

            var updateRequest = new UpdateBookReviewRequest
            {
                Id = 1,
                Comment = "",
                Score = 5.1F
            };

            Assert.That(() => _bookReviewService.Update(updateRequest),
                Throws.InstanceOf<Exception>().With.Message.EqualTo("The score must be between 0 and 5"));
        }
    }
}
