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
    public class BookController : ControllerBase
    {
        private readonly BookRepository _repository;

        public BookController()
        {
            _repository = new BookRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var books = _repository.GetAll();
            return Ok(books);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Book book)
        {
            _repository.Add(book);
            _repository.SaveChanges();
            return Ok();
        }
    }
}
