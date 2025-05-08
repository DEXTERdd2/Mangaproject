using MangaBackend.Domain.Interfaces.ITb_CommentService;
using MangaBackend_Application.DTOS.Tb_CommentDto;
using Microsoft.AspNetCore.Mvc;

namespace MangaBackend.Api.Controllers.Tb_CommentsController
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [HttpGet("getcomment")]
        public IActionResult GetComments([FromQuery] string mangaSlug, [FromQuery] string chapterSlug)
        {
            try
            {
                var response = _commentService.GetComments(mangaSlug, chapterSlug);
                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An unexpected error occurred",
                    exception = ex.Message
                });
            }
        }

      
        [HttpPost("addcomment")]
        public IActionResult AddComment([FromForm] CommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid data" });

            try
            {
                var response = _commentService.AddComment(dto);
                return StatusCode(response.statusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An unexpected error occurred",
                    exception = ex.Message
                });
            }
        }
    }
}
