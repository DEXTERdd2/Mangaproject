using ArcadeGameBackend.Application.MiddleWare;
using MangaBackend.Domain.Interfaces.IAuthService;
using MangaBackend_Application.Common.ResponseType;
using MangaBackend_Application.DTOS.AuthDtos;
using Microsoft.AspNetCore.Mvc;

namespace MangaBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginRequest request)
        {
            try
            {
                var response = _authService.Login(request);

                if (response.DataModel == null)
                {
                    return Unauthorized(ResponseData.NotSuccessResponse("Invalid email/username or password."));
                }

                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseData.ErrorResponse(string.Empty, ex.Message));
            }
        }

    }

}

