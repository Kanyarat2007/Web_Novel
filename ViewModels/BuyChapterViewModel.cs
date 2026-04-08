using Web_Novel.Models;
using System.Collections.Generic;

// ใช้แสดง “รายการคนที่เราบล็อค”
// เอาไว้แสดงในหน้า “Blocked Users List”

namespace Web_Novel.ViewModels

{
public class BuyChapterViewModel
{
    public int ChapterId { get; set; }
    public string ChapterTitle { get; set; }

    public int Price { get; set; }

    public int CurrentBalance { get; set; }

    public bool IsPurchased { get; set; }
}
}