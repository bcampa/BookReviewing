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
        private readonly IUserRepository _userRepository;

        public BookReviewService(IBookReviewRepository bookReviewRepository, IUserRepository userRepository)
        {
            _bookReviewRepository = bookReviewRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<BookReviewDto> GetByFilter(BookReviewFilter filter)
        {
            var entities = _bookReviewRepository.GetByFilter(filter);
            var dtos = new List<BookReviewDto>();

            foreach (var entity in entities)
                dtos.Add(MapEntityToDto(entity));

            return dtos;
        }

        public BookReviewDto GetById(int id)
        {
            var entity = _bookReviewRepository.GetById(id);
            return MapEntityToDto(entity);
        }

        public BookReviewDto Add(CreateBookReviewRequest request)
        {
            var currentTime = DateTime.Now;

            var userGuid = Guid.Parse(request.UserGuid);
            var user = _userRepository.GetByGuid(userGuid);

            if (user == null)
            {
                throw new Exception("No user with such GUID");
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
            var entity = _bookReviewRepository.GetById(request.Id);

            entity.Score = request.Score;
            entity.Comment = request.Comment;
            entity.LastUpdate = DateTime.Now;

            _bookReviewRepository.Update(entity);
            _bookReviewRepository.SaveChanges();

            return MapEntityToDto(entity);
        }

        public void Delete(int id)
        {
            _bookReviewRepository.DeleteById(id);
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
    }
}
