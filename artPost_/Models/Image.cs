using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace artPost_.Models
{
    [Table("Image")]
    public class Image
    {
        int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        
       // [NotMapped]
        [Required]
        public byte[] image { get; set; }

        public int imageId { get; set; }

        public string ownerId { get; set; }

        public int likes { get; set; }

        public string likeCount { get; set; } 

        //maybe add likes and tags feature?
    }
}
