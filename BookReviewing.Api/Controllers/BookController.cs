using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Contracts;
using BookReviewing.Services.Dto.Book;
using BookReviewing.Services.Dto.Misc;
using BookReviewing.Shared.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets multiple books according to the submitted filter. This endpoint should only be used for debugging.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>The books matching the filter</returns>
        /// <response code="200">Returns the books matching the filter</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<Book>), StatusCodes.Status200OK)]
        public IActionResult Get([FromQuery] PaginationFilter filter)
        {
            filter ??= new PaginationFilter();

            var books = _repository.GetMany(filter);
            var response = new PaginatedResponse<Book>(filter, books);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new Book. This endpoint should only be used for debugging.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A newly created book</returns>
        /// <response code="200">Returns the newly created book</response>
        [HttpPost]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        public IActionResult Add([FromBody] CreateBookRequest request)
        {
            var book = new Book { Id = request.Id };

            _repository.Add(book);
            _repository.SaveChanges();
            return Ok(book);
        }
    }
}
