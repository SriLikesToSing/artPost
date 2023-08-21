using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace artPost.Models
{
    [Table("userList")]
    public class userList
    {
        public int Id { get; set; }
        public ICollection<user> users { get; set; }
        [Required]
        public int userCount { get; set; }
    }
}
