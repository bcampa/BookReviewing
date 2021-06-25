using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Contracts;
using BookReviewing.Services.Dto.Misc;
using BookReviewing.Services.Dto.User;
using BookReviewing.Shared.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookReviewing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets multiple users according to the submitted filter. This endpoint should only be used for debugging.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>The users matching the filter</returns>
        /// <response code="200">Returns the users matching the filter</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<User>), StatusCodes.Status200OK)]
        public IActionResult Get([FromQuery] PaginationFilter filter)
        {
            filter ??= new PaginationFilter();

            var users = _repository.GetMany(filter);
            var response = new PaginatedResponse<User>(filter, users);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new User. This endpoint should only be used for debugging.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A newly created user</returns>
        /// <response code="200">Returns the newly created user</response>
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public IActionResult Add([FromBody] CreateUserRequest request)
        {
            var user = new User
            {
                Guid = request.Guid,
                Name = request.Name
            };

            _repository.Add(user);
            _repository.SaveChanges();
            return Ok(user);
        }
    }
}
