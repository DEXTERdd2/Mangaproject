using MangaBackend.Domain.Interfaces.ITb_UserService;
using MangaBackend_Application.DTOS.Tb_UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace MangaBackend.Api.Controllers.Tb_UserController
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var response = _service.GetAllUsers();
                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred", Exception = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var response = _service.GetUserById(id);
                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred", Exception = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromForm] UserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var response = _service.AddUser(dto);
                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred", Exception = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] UserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var response = _service.UpdateUser(id, dto);
                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred", Exception = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var response = _service.DeleteUser(id);
                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred", Exception = ex.Message });
            }
        }
    }
}

