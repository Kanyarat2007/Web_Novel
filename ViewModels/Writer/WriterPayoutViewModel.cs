using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Writer
{
    public class WriterPayoutViewModel
    {
        public int PayoutId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}