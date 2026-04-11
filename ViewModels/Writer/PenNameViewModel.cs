using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Writer
{
    public class PenNameViewModel
    {
        public int PenNameId { get; set; }
        public string PenName { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }

        public bool IsDefault { get; set; }
    }
}