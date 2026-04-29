using Microsoft.AspNetCore.Mvc;

namespace stepik_API.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly CoursesService service;
        public CoursesController(CoursesService coursesService)
        {
            service = coursesService;
        }
        [HttpGet("GetCourses")]
        public IActionResult GetCourses(string fullName)
        {
            List<Course> courses = service.Get(fullName);
            return (courses != null && courses.Any()) ? Ok(courses) : NotFound("Not found");
        }
        [HttpGet("GetTotalCountCourses")]
        public IActionResult GetTotalCountCourses()
        {
            return Ok(service.GetTotalCount());
        }
    }
}
