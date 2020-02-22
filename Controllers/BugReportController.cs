using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        public IEnumerable<BugReport> Get([FromQuery] string playerID, [FromQuery] string date, [FromQuery] string description, [FromQuery] string logs)
        {
            using (var context = new ApplicationDbContext())
            {
                IEnumerable<BugReport> reports = context.BugReports;
                if (playerID != null)
                {
                    reports = reports.Where(r => r.PlayerID == Convert.ToInt32(playerID));
                }
                if (date != null)
                {
                    var parsedDate = DateTime.ParseExact(date, "yyyyMMdd", null);
                    reports = reports.Where(r => r.Date >= parsedDate && r.Date < parsedDate.AddDays(1.0));
                }
                if (description != null)
                {
                    reports = reports.Where(r => r.BugDescription.Contains(description));
                }
                if (logs != null)
                {
                    reports = reports.Where(r => r.Logs.Contains(logs));
                }
                return reports.ToList();
            }
        }
        [HttpPost]
        public BugReport Save([FromBody] BugReport report)
        {
            var playerID = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            report.PlayerID = playerID;
            using (var context = new ApplicationDbContext())
            {
                context.BugReports.Add(report);
                context.SaveChanges();
                return report;
            }
        }
    }
}
