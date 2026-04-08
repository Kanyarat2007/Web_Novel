using Web_Novel.Models;

namespace Web_Novel.ViewModels
{
    public class BookshelfViewModel
    {
        public int UserId { get; set; }

        // tab: shelf
        public List<Novel> Novels { get; set; } = new();
        public List<Manga> Mangas { get; set; } = new();

        // tab: bookmark
        public List<Chapterbookmark> Bookmarks { get; set; } = new();

        // tab: recent
        public List<Readinghistory> RecentNovels { get; set; } = new();
    }
}