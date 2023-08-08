﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace artPost.Models
{
    [Table("user")]
    public class user
    {
        public int Id { get; set; }
        [Required]

        public string userId { get; set; } //gets assigned random hash or individual user id dunno
        [Required]
        public string userName { get; set; }

        public List<string> images { get; set; }

        public string description { get; set; }

        public string profilePicLink { get; set; }

        [Required]
        public int followers { get; set; }
    }
}
