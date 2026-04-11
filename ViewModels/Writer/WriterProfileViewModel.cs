using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels

{
    public class WriterProfileViewModel

    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        // mock payment
        public bool IsPaymentConfirmed { get; set; }
    }
}