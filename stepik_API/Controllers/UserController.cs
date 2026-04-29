using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace stepik_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UsersService service;

        public UserController(UsersService usersService)
        {
            service = usersService;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {
            bool wasAdded = service.Add(user);
            return wasAdded ? Ok("Added") : BadRequest("Can't added");
        }
        [HttpGet("GetUser")]
        public IActionResult GetUser(string fullName)
        {
            User? user = service.Get(fullName);
            return user != null ? Ok(user) : NotFound("Not found");
        }
        [HttpGet("GetTotalUserCount")]
        public IActionResult GetTotalUserCount()
        {
            return Ok(service.GetTotalCount());
        }
        [HttpGet("FormatUserMetrics")]
        public IActionResult FormatUserMetrics(int number)
        {
            string? format = service.FormatUserMetrics(number);
            return format != null ? Ok(format) : BadRequest("Not correct fomrmat");
        }
        [HttpGet("GetUserRating")]
        public IActionResult GetUserRating()
        {
            DataSet dataSet = service.GetUserRating();
            var rating = dataSet
                .Tables[0]
                .Rows
                .Cast<DataRow>()
                .Select(row => new
                    {
                        FullName = row["full_name"].ToString(),
                        Knowledge = Convert.ToInt32(row["knowledge"]),
                        Reputation = Convert.ToInt32(row["reputation"])
                    })
                .ToList();
            return (rating != null && rating.Any()) ? Ok(rating) : NotFound("Not found");
        }
    } 
}
