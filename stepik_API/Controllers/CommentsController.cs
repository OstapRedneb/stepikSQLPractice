using Microsoft.AspNetCore.Mvc;

namespace stepik_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsService service;
        public CommentsController(CommentsService commentsService)
        {
            service = commentsService;
        }

        [HttpGet("GetComments")]
        public IActionResult GetComments(int id)
        {
            List<Comment> comments = service.Get(id);
            return (comments != null && comments.Any()) ? Ok(comments) : NotFound("Комментариев не найдено");
        }
        [HttpDelete("DeleteComments")]
        public IActionResult DeleteComments(int id)
        {
            bool wasDelete = service.Delete(id);
            return wasDelete ? Ok("Удалено") : BadRequest("Ошибка");
        }
    }
}
