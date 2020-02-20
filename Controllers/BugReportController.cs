using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BugReportModule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugReportController : ControllerBase
    {
        private readonly ILogger<BugReportController> _logger;

        public BugReportController(ILogger<BugReportController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
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
