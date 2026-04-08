using Web_Novel.Models;
using System.Collections.Generic;

namespace Web_Novel.ViewModels
{
    public class BlockUserViewModel
    {
        public int BlockedUserId { get; set; }
        public int BlockerUserId { get; set; }

    }
}