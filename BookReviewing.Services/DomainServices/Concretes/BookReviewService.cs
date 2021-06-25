using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories;
using BookReviewing.Entities.Repositories.Contracts;
using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Dto.BookReview;
using BookReviewing.Services.Dto.User;
using BookReviewing.Shared.Filters;
using System;
using System.Collections.Generic;

namespace BookReviewing.Services.DomainServices.Concretes
{
    public class BookReviewService : IBookReviewService
    {
        private readonly IBookReviewRepository _bookReviewRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public BookReviewService(
            IBookReviewRepository bookReviewRepository,
            IBookRepository bookRepository,
            IUserRepository userRepository)
        {
            _bookReviewRepository = bookReviewRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<BookReviewDto> GetByFilter(BookReviewFilter filter)
        {
            var entities = _bookReviewRepository.GetByFilter(filter);
            var dtos = new List<BookReviewDto>();

            if (entities != null)
                foreach (var entity in entities)
                    dtos.Add(MapEntityToDto(entity));

            return dtos;
        }

        public BookReviewDto GetById(int id)
        {
            var entity = _bookReviewRepository.GetById(id);

            if (entity == null)
            {
                throw new Exception("Book review not found");
            }

            return MapEntityToDto(entity);
        }

        public BookReviewDto Add(CreateBookReviewRequest request)
        {
            var currentTime = DateTime.Now;

            ValidateScore(request.Score);
            ValidateComment(request.Comment);

            var book = _bookRepository.GetById(request.BookId);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            var userGuid = Guid.Parse(request.UserGuid);
            var user = _userRepository.GetByGuid(userGuid);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var entity = new BookReview
            {
                BookId     = request.BookId,
                UserId     = user.Id,
                Score      = request.Score,
                Comment    = request.Comment,
                DatePosted = currentTime,
                LastUpdate = currentTime
            };

            _bookReviewRepository.Add(entity);
            _bookReviewRepository.SaveChanges();

            return MapEntityToDto(entity);
        }

        public BookReviewDto Update(UpdateBookReviewRequest request)
        {
            ValidateScore(request.Score);
            ValidateComment(request.Comment);

            var entity = _bookReviewRepository.GetById(request.Id);

            if (entity == null)
            {
                throw new Exception("Book review not found");
            }

            entity.Score = request.Score;
            entity.Comment = request.Comment;
            entity.LastUpdate = DateTime.Now;

            _bookReviewRepository.Update(entity);
            _bookReviewRepository.SaveChanges();

            return MapEntityToDto(entity);
        }

        public void Delete(int id)
        {
            var entity = _bookReviewRepository.GetById(id);

            if (entity == null)
            {
                throw new Exception("Book review not found");
            }

            _bookReviewRepository.Delete(entity);
            _bookReviewRepository.SaveChanges();
        }

        private BookReviewDto MapEntityToDto(BookReview entity)
        {
            var dto = new BookReviewDto
            {
                Id = entity.Id,
                BookId = entity.BookId,
                Score = entity.Score,
                Comment = entity.Comment,
                DatePosted = entity.DatePosted,
                LastUpdate = entity.LastUpdate
            };

            if (entity.User != null)
            {
                dto.User = new UserDto
                {
                    Guid = entity.User.Guid,
                    Name = entity.User.Name
                };
            }

            return dto;
        }

        private void ValidateScore(float score)
        {
            if (score < 0 || score > 5)
                throw new Exception("The score must be between 0 and 5");
        }

        private void ValidateComment(string comment)
        {
            if (string.IsNullOrEmpty(comment))
                throw new Exception("Comment must not be empty");

            if (comment.Length > 4000)
                throw new Exception("Comments cannot exceed 4000 characters");
        }
    }
}
