using System.ComponentModel.DataAnnotations;

namespace artPost_.Models
{
    public class Image
    {
        int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string image { get; set; }

        public int imageId { get; set; }

        //maybe add likes feature?
    }
}
