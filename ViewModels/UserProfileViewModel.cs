using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels
{
    public class UserProfileViewModel
    {
        public string Username { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImage { get; set; }
        public IFormFile? ProfileImageFile { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsCurrentUser { get; set; }
    }
}