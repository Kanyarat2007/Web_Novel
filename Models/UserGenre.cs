using System;
using System.Collections.Generic;


namespace Web_Novel.Models
{
    public class UserGenre
    {
        public int UserId { get; set; }
        public int GenreId { get; set; }

        public User? User { get; set; }
        public Genre? Genre { get; set; }
    }
}
