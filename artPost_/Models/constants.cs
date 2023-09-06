using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace artPost_.Models
{
    [Table("constants")]
    public class constants
    {
        public int Id { get; set; }

        public Random rnd = new Random(); 

    }
}
