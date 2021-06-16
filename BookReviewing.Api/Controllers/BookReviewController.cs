using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories;
using BookReviewing.Services.DomainServices;
using BookReviewing.Services.Dto.BookReview;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookReviewing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookReviewController : ControllerBase
    {
        private readonly BookReviewService _service;

        public BookReviewController()
        {
            _service = new BookReviewService();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var bookReviews = _service.GetAll();
            return Ok(bookReviews);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var bookReviews = _service.GetById(id);
            return Ok(bookReviews);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateBookReviewRequest request)
        {
            BookReviewDto response = _service.Add(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateBookReviewRequest request)
        {
            BookReviewDto response = _service.Update(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
