using Web_Novel.Models;

namespace Web_Novel.ViewModels
{
    public class SearchViewModel
    {
        public string Query { get; set; } = string.Empty;
        public List<Novel> Novels { get; set; } = new();
    }
}