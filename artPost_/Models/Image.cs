using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace artPost_.Models
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        // [NotMapped]
        [Required]
        public byte[] image { get; set; }
        [Required]
        public string ownerId { get; set; }
        [Required]
        public int likes { get; set; }
        [Required]
        public string iID { get; set; }
        [Required]
        public string likeCount { get; set; } 

        //maybe add likes and tags feature?
    }
}
