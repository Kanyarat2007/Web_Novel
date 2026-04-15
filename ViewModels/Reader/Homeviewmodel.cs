using Web_Novel.Models;

namespace Web_Novel.ViewModels.Reader
{
    public class HomeViewModel
    {
        public int UserId { get; set; }
        public bool IsLoggedIn => UserId > 0;

        // ประวัติการอ่านล่าสุด (เฉพาะ login)
        public List<Readinghistory> RecentNovels { get; set; } = new();

        // หมวดหมู่ที่ผู้ใช้สนใจ + นิยายในหมวดนั้น (เฉพาะ login)
        public List<GenreWithNovels> InterestedGenres { get; set; } = new();

        // นิยายแนะนำทั่วไป (Guest เห็นทุกหมวด, Member เห็นหมวดที่ไม่สนใจ)
        public List<GenreWithNovels> AllGenres { get; set; } = new();

        public int CurrentBalance { get; set; }

        public bool CanClaimDaily { get; set; }

        public int CurrentStreak { get; set; }

        public int TodayReward { get; set; }

        public List<BannerViewModel> Banners { get; set; } = new();
    }

    public class GenreWithNovels
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; } = string.Empty;
        public List<Novel> Novels { get; set; } = new();
    }
}