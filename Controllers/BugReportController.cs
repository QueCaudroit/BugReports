using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace BugReportModule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BugReportController : ControllerBase
    {
        private readonly ILogger<BugReportController> _logger;

        public BugReportController(ILogger<BugReportController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IEnumerable<BugReport> Get()
        {
            using (var context = new ApplicationDbContext())
            {
                var reports = context.BugReports.ToList();
                return reports;
            }
        }
        [HttpPost]
        public BugReport Save([FromBody] BugReport report)
        {
            using (var context = new ApplicationDbContext())
            {
                context.BugReports.Add(report);
                context.SaveChanges();
                return report;
            }
        }
    }
}
