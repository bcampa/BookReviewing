using BookReviewing.Services.DomainServices.Contracts;
using BookReviewing.Services.Dto.BookReview;
using BookReviewing.Services.Dto.Misc;
using BookReviewing.Shared.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewing.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookReviewController : ControllerBase
    {
        private readonly IBookReviewService _service;

        public BookReviewController(IBookReviewService service)
        {
            _service = service;
        }


        /// <summary>
        /// Gets multiple reviews according to the submitted filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>The reviews matching the filter</returns>
        /// <response code="200">Returns the reviews matching the filter</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<BookReviewDto>), StatusCodes.Status200OK)]
        public IActionResult Get([FromQuery] BookReviewFilter filter)
        {
            filter ??= new BookReviewFilter();

            var bookReviews = _service.GetByFilter(filter);
            var response = new PaginatedResponse<BookReviewDto>(filter, bookReviews);
            return Ok(response);
        }

        /// <summary>
        /// Gets a BookReview matching the provided ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The review corresponding to the ID</returns>
        /// <response code="200">Returns the review corresponding to the ID</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookReviewDto), StatusCodes.Status200OK)]
        public IActionResult GetById([FromRoute] int id)
        {
            BookReviewDto response = _service.GetById(id);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new BookReview.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A newly created review</returns>
        /// <response code="201">Returns the newly created review</response>
        [HttpPost]
        [ProducesResponseType(typeof(BookReviewDto), StatusCodes.Status201Created)]
        public IActionResult Add([FromBody] CreateBookReviewRequest request)
        {
            BookReviewDto response = _service.Add(request);
            var routeParams = new { id = response.Id };

            return CreatedAtAction(
                nameof(GetById),
                routeParams,
                response);
        }


        /// <summary>
        /// Updates a BookReview.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The updated review.</returns>
        /// <response code="200">Returns the updated review</response>
        [HttpPut]
        [ProducesResponseType(typeof(BookReviewDto), StatusCodes.Status200OK)]
        public IActionResult Update([FromBody] UpdateBookReviewRequest request)
        {
            BookReviewDto response = _service.Update(request);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a BookReview.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">If the deletion is successful</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete([FromRoute] int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}
