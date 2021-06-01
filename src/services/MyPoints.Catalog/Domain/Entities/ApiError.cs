using System.Collections.Generic;

namespace MyPoints.Catalog.Domain.Entities
{
    public class ApiError
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string TraceId { get; set; }
        public string Error { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
