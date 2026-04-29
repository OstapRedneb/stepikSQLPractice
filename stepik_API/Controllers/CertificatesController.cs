using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace stepik_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CertificatesController : ControllerBase
    {
        private readonly CertificatesService service;
        public CertificatesController(CertificatesService certificatesService)
        {
            service = certificatesService;
        }
        [HttpGet("GetCertificates")]
        public IActionResult GetCertificates(string fullName)
        {
            DataSet dataSet = service.Get(fullName);
            var certificates = dataSet
                .Tables[0]
                .Rows
                .Cast<DataRow>()
                .Select(row => new
                    {
                        CourseTitle = row["title"].ToString(),
                        IssueDate = Convert.ToDateTime(row["issue_date"]),
                        Grade = Convert.ToInt32(row["grade"])
                    })
                .ToList();
            return (certificates != null && certificates.Any()) ? Ok(certificates) : NotFound("Not found");
        }
    }
}
