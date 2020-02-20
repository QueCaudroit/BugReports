using System;

namespace BugReportModule
{
    public class BugReport
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; }

        public int PlayerID { get; set; }

        public string BugDescription { get; set; }
        public string Logs { get; set; }
        public BugReport()
        {          
            this.Date = DateTime.UtcNow;
        }
    }
}
