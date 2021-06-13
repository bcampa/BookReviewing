using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult Get()
        {
            var bookReviews = _repository.GetAll();
            return Ok(bookReviews);
        }

        [HttpPost]
        public IActionResult Add([FromBody] BookReview bookReview)
        {
            _repository.Add(bookReview);
            _repository.SaveChanges();
            return Ok();
        }
    }
}
