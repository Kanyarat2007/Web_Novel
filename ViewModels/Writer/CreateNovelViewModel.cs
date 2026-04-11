// ViewModels/NovelDetailViewModel.cs
using Web_Novel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Novel.Models.Enums;

namespace Web_Novel.ViewModels.Writer
{
    public class CreateNovelViewModel
    {
        public int? NovelId { get; set; }
        public bool IsEdit => NovelId > 0;

        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        public string? AuthorNote { get; set; }
        public IFormFile? CoverImage { get; set; }
        public string? ExistingCoverImageUrl { get; set; }
        public int GenreId { get; set; }
        public int PenNameId { get; set; }
        public NovelStatus Status { get; set; }
        public string SeriesType { get; set; } = null!;
        public int TotalViews { get; set; }

        // public string PublishStatus { get; set; } = null!;
        public string PublishStatus { get; set; } = "ongoing";

        // public List<SelectListItem> Genres { get; set; } = new();
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public string Slug { get; set; } = null!;
        public List<Penname> PenNames { get; set; } = new List<Penname>();

        public List<NovelTriggerWarning> NovelTriggerWarning { get; set; } = new List<NovelTriggerWarning>();
        public List<TriggerWarning> TriggerWarnings { get; set; } = new List<TriggerWarning>();
        public List<TriggerWarning> SelectedTriggerWarnings { get; set; } = new List<TriggerWarning>();
        public List<int> TriggerWarningIds { get; set; } = new();


        // public virtual ICollection<NovelTriggerWarning> NovelTriggerWarnings { get; set; }
    }
}