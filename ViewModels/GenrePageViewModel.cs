using Web_Novel.Models;

namespace Web_Novel.ViewModels
{
    public class GenrePageViewModel
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; } = string.Empty;

        public List<Novel> Novels { get; set; } = new();

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}