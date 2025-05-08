using MangaBackend.Domain.Interfaces.IMangaService;
using MangaBackend_Application.DTOS.MangaDtos;
using Microsoft.AspNetCore.Mvc;

namespace MangaBackend.Api.Controllers.MangaController
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangaController : ControllerBase
    {
        private readonly IMangaService _service;

        public MangaController(IMangaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var response = _service.GetAllMangas();
                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", exception = ex.Message });
            }
        }

        [HttpGet("{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            try
            {
                var response = _service.GetMangaBySlug(slug);
                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", exception = ex.Message });
            }
        }

        [HttpGet("featured")]
        public IActionResult GetFeatured()
        {
            try
            {
                var response = _service.GetFeaturedMangas();
                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", exception = ex.Message });
            }
        }

        [HttpPost("add")]
        public IActionResult AddManga([FromBody] MangaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid data" });

            try
            {
                var response = _service.AddManga(dto);
                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", exception = ex.Message });
            }
        }

    }
}
