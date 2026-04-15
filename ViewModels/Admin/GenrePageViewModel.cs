using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels.Admin
{
    public class GenreViewModel
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }

    public class GenrePageViewModel
    {
        public List<GenreViewModel> Genres { get; set; } = new();

        // สำหรับ Add/Edit
        public GenreViewModel Form { get; set; } = new();
    }
}