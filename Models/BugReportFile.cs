using System;
using System.Text.Json.Serialization;

namespace BugReportModule
{
    public class BugReportFile
    {
        public Guid ID { get; set; }
        
        [JsonIgnore]
        public BugReport BugReport { get; set; }
        public Guid BugReportID { get; set; }

        public string Filename { get; set; }
        public string Path { get; set; }
    }
}
