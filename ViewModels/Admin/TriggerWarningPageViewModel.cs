using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Admin
{
    public class TriggerWarningViewModel
    {
        public int WarningId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TriggerWarningPageViewModel
    {
        public List<TriggerWarningViewModel> Warnings { get; set; } = new();
        public TriggerWarningViewModel Form { get; set; } = new();
        public string SortBy { get; set; }
    }
}