using System;

namespace Web_Novel.Models
{
    public class WriterPayout
    {
        public int PayoutId { get; set; }

        public int UserId { get; set; }
        public decimal Amount { get; set; }

        public string Status { get; set; } = "Pending"; 
        // Pending, Approved, Rejected, Paid

        public DateTime RequestedAt { get; set; } = DateTime.Now;
        public DateTime? ApprovedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        public int? ApprovedBy { get; set; }

        // Navigation
        public virtual User User { get; set; } = null!;
        public virtual User? Admin { get; set; }
    }
}