using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories;
using BookReviewing.Services.Dto.BookReview;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookReviewing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookReviewController : ControllerBase
    {
        private readonly BookReviewRepository _repository;

        public BookReviewController()
        {
            _repository = new BookReviewRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var bookReviews = _repository.GetAll();
            return Ok(bookReviews);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var bookReviews = _repository.GetById(id);
            return Ok(bookReviews);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateBookReviewRequest request)
        {
            var currentTime = DateTime.Now;

            var bookReview = new BookReview
            {
                BookId = request.BookId,
                UserId = request.UserId,
                Score = request.Score,
                Comment = request.Comment,
                DatePosted = currentTime,
                LastUpdate = currentTime
            };

            _repository.Add(bookReview);
            _repository.SaveChanges();
            return Ok(bookReview);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateBookReviewRequest request)
        {
            var bookReview = _repository.GetById(request.Id);

            bookReview.Score = request.Score;
            bookReview.Comment = request.Comment;
            bookReview.LastUpdate = DateTime.Now;

            _repository.Update(bookReview);
            _repository.SaveChanges();
            return Ok(bookReview);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _repository.DeleteById(id);
            _repository.SaveChanges();
            return Ok();
        }
    }
}
