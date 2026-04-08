using Web_Novel.Models;
using System.Collections.Generic;

// ใช้แสดง “รายการคนที่เราบล็อค”
// เอาไว้แสดงในหน้า “Blocked Users List”

namespace Web_Novel.ViewModels

{
    public class TopUpViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public int CurrentBalance { get; set; }

        public List<Coinpackage> Packages { get; set; }
    }
}