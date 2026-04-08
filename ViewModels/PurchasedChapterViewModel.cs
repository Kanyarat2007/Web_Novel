using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels

{
    public class PurchasedChapterViewModel
    {
        public int NovelId { get; set; }
        public string NovelTitle { get; set; }
        public int ChapterId { get; set; }
        public int ChapterNumber { get; set; }
        public string ChapterTitle { get; set; }
        public int CoinSpent { get; set; }
        public DateTime PurchasedAt { get; set; }
    }

    public class PurchasedNovelGroupViewModel
    {
        public int NovelId { get; set; }
        public string NovelTitle { get; set; }
        public string? CoverImageUrl { get; set; }
        public List<PurchasedChapterViewModel> Chapters { get; set; } = new List<PurchasedChapterViewModel>();
    }

    public class PurchaseHistoryViewModel
    {
        public List<PurchasedNovelGroupViewModel> NovelGroups { get; set; } = new List<PurchasedNovelGroupViewModel>();
    }
}