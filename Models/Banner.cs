using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Novel.Models
{
    public class Banner
    {
        public int BannerId { get; set; }

        [MaxLength(200)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; } = null!;

        [MaxLength(500)]
        public string? RedirectUrl { get; set; }

        [Required]
        [MaxLength(50)]
        public string Position { get; set; } = "HomepageTop";

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}