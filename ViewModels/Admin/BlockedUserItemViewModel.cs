using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Admin

{
    public class ReportAdminViewModel
    {
        public int ReportId { get; set; }
        public string TargetType { get; set; } = "";
        public int TargetId { get; set; }

        public string Reason { get; set; } = "";
        public string? Description { get; set; }

        public string ReporterName { get; set; } = "";
        public DateTime CreatedAt { get; set; }

        public string Status { get; set; } = "pending";

        // แสดงข้อมูล target
        public string? TargetContent { get; set; } // comment content / novel title
    }
}