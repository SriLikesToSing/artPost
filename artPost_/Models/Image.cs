using System.ComponentModel.DataAnnotations;

namespace artPost_.Models
{
    public class Image
    {
        int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile image { get; set; }

        public int imageId { get; set; }

        public string ownerId { get; set; }

        //maybe add likes and tags feature?
    }
}
