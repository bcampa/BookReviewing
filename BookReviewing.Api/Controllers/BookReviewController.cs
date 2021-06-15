using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var bookReviews = _repository.GetById(id);
            return Ok(bookReviews);
        }

        [HttpPost]
        public IActionResult Add([FromBody] BookReview bookReview)
        {
            _repository.Add(bookReview);
            _repository.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] BookReview bookReview)
        {
            _repository.Update(bookReview);
            _repository.SaveChanges();
            return Ok();
        }

        [HttpDelete("/{id}"]
        public IActionResult Delete([FromRoute] int id)
        {
            _repository.DeleteById(id);
            _repository.SaveChanges();
            return Ok();
        }
    }
}
