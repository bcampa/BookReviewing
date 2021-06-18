using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories;
using BookReviewing.Services.Dto.Book;
using BookReviewing.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get([FromQuery] PaginationFilter filter)
        {
            filter ??= new PaginationFilter();

            var books = _repository.GetMany(filter);
            return Ok(books);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateBookRequest request)
        {
            var book = new Book { Id = request.Id };

            _repository.Add(book);
            _repository.SaveChanges();
            return Ok(book);
        }
    }
}
