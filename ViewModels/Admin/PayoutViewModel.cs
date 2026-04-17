using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Admin
{
    public class PayoutViewModel
    {
        public int PayoutId { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }

        public decimal Amount { get; set; }
        public string Status { get; set; }

        public DateTime RequestedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}