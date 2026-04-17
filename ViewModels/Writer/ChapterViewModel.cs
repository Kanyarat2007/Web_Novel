// ViewModels/NovelDetailViewModel.cs
using Web_Novel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Novel.Models.Enums;

namespace Web_Novel.ViewModels.Writer
{
    public class ChapterViewModel
    {
        public int ChapterId { get; set; }
        public int NovelId { get; set; }
        public int ChapterNumber { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public bool IsCoinRequired { get; set; }
        public int CoinPrice { get; set; }

        public string PublishStatus { get; set; }

        public bool IsEdit { get; set; }

        // ตัวเลือกทั้งหมด
        public List<TriggerWarningOption> AllWarnings { get; set; } = new();

        // ตัวที่เลือก
        public List<int> SelectedWarningIds { get; set; } = new();
    }

    public class TriggerWarningOption
    {
        public int WarningId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}