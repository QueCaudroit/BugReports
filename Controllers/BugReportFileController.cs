using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace BugReportModule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BugReportFileController : ControllerBase
    {
        private readonly ILogger<BugReportFileController> _logger;

        public BugReportFileController(ILogger<BugReportFileController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{fileID}")]
        [Authorize(Roles = "admin")]
        public IActionResult Get(string fileId)
        {
            using (var context = new ApplicationDbContext())
            {
                BugReportFile file = context.BugReportFiles.Find(Guid.Parse(fileId));
                return PhysicalFile(file.Path, "application/octet-stream", file.Filename);
            }
        }

        [HttpPost]
        public BugReport Save([FromQuery] string reportID)
        {
            var files = HttpContext.Request.Form.Files;
            using (var context = new ApplicationDbContext())
            {
                var report = context.BugReports.Find(Guid.Parse(reportID));
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                        var newFilename = Convert.ToString(Guid.NewGuid());
                        var path = $"/uploadedFiles/{newFilename}";
                        using (FileStream fs = System.IO.File.Create(path))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        BugReportFile fileDb = new BugReportFile
                        {
                            BugReport = report,
                            Filename = fileName,
                            Path = path
                        };
                        context.BugReportFiles.Add(fileDb);
                    }
                }
                context.SaveChanges();
                context.Entry(report).Collection(r => r.BugReportFiles).Load();
                return report;
            }
        }
    }
}
