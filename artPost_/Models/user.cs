using artPost_.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace artPost.Models
{
    [Table("user")]
    public class user
    {
        public int Id { get; set; }
        [Required]
        public string userName { get; set; }

        [NotMapped]
        public List<Image> images { get; set; } = new List<Image>();

        public string description { get; set; }

        public byte profilePic { get; set; } 

        [Required]
        public int followers { get; set; }

        public bool isProfileCreated { get; set; } = false;
    }
}
