using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels
{
    public class WriterDashboardViewModel
    {
        public List<MyNovelItemViewModel> Novels { get; set; } = new();
        public List<PenNameViewModel> PenNames { get; set; } = new();
        public List<WriterPaymentViewModel> Payments { get; set; } = new();

        public string ActiveTab { get; set; }
    }
}