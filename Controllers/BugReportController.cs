using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<BugReport> Get([FromQuery] string playerId, [FromQuery] string date, [FromQuery] string description, [FromQuery] string logs)
        {
            using (var context = new ApplicationDbContext())
            {
                IEnumerable<BugReport> reports = context.BugReports.Include(report => report.BugReportFiles);
                if (playerId != null)
                {
                    reports = reports.Where(r => r.PlayerID == Convert.ToInt32(playerId));
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
            var playerId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            report.PlayerID = playerId;
            using (var context = new ApplicationDbContext())
            {
                context.BugReports.Add(report);
                context.SaveChanges();
                context.Entry(report).Reload();
                context.Entry(report).Collection(report => report.BugReportFiles).Load();
                return report;
            }
        }
    }
}
