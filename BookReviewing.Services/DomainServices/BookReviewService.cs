using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories;
using BookReviewing.Services.Dto.BookReview;
using BookReviewing.Shared.Filters;
using System;
using System.Collections.Generic;

namespace BookReviewing.Services.DomainServices
{
    public class BookReviewService
    {
        private readonly BookReviewRepository _repository;

        public BookReviewService()
        {
            _repository = new BookReviewRepository();
        }

        public IEnumerable<BookReviewDto> GetByFilter(BookReviewFilter filter)
        {
            var entities = _repository.GetByFilter(filter);
            var dtos = new List<BookReviewDto>();

            foreach (var entity in entities)
                dtos.Add(MapEntityToDto(entity));

            return dtos;
        }

        public BookReviewDto GetById(int id)
        {
            var entity = _repository.GetById(id);
            return MapEntityToDto(entity);
        }

        public BookReviewDto Add(CreateBookReviewRequest request)
        {
            var currentTime = DateTime.Now;

            var entity = new BookReview
            {
                BookId     = request.BookId,
                UserId     = request.UserId,
                Score      = request.Score,
                Comment    = request.Comment,
                DatePosted = currentTime,
                LastUpdate = currentTime
            };

            _repository.Add(entity);
            _repository.SaveChanges();

            return MapEntityToDto(entity);
        }

        public BookReviewDto Update(UpdateBookReviewRequest request)
        {
            var entity = _repository.GetById(request.Id);

            entity.Score = request.Score;
            entity.Comment = request.Comment;
            entity.LastUpdate = DateTime.Now;

            _repository.Update(entity);
            _repository.SaveChanges();

            return MapEntityToDto(entity);
        }

        public void Delete(int id)
        {
            _repository.DeleteById(id);
        }

        private BookReviewDto MapEntityToDto(BookReview entity)
        {
            return new BookReviewDto
            {
                Id = entity.Id,
                BookId = entity.BookId,
                UserId = entity.UserId,
                Score = entity.Score,
                Comment = entity.Comment,
                DatePosted = entity.DatePosted,
                LastUpdate = entity.LastUpdate
            };
        }
    }
}
