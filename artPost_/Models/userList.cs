using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace artPost.Models
{
    [Table("useList")]
    public class userList
    {
        public int Id { get; set; }

        public List<user> users { get; set; }

        public int userCount { get; set; }
    }
}
