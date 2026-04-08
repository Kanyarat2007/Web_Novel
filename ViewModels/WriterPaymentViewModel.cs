using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels
{
    public class WriterPaymentViewModel
    {
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        // ข้อมูลธนาคาร
        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }

        // mock payment
        public bool IsPaymentConfirmed { get; set; }

        // optional (เผื่อใช้)
        public string? PromptPay { get; set; }
    }
}