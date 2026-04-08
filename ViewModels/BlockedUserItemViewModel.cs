using Web_Novel.Models;
using System.Collections.Generic;

// ใช้แสดง “รายการคนที่เราบล็อค”
// เอาไว้แสดงในหน้า “Blocked Users List”

namespace Web_Novel.ViewModels

{
    public class BlockedUserItemViewModel
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string? ProfileImage { get; set; }
    public DateTime BlockedAt { get; set; }
}
}