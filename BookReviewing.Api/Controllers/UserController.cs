using BookReviewing.Entities.Models;
using BookReviewing.Entities.Repositories.Contracts;
using BookReviewing.Services.Dto.User;
using BookReviewing.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult Get([FromQuery] PaginationFilter filter)
        {
            filter ??= new PaginationFilter();

            var users = _repository.GetMany(filter);
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateUserRequest request)
        {
            var user = new User { Id = request.Id };

            _repository.Add(user);
            _repository.SaveChanges();
            return Ok(user);
        }
    }
}
