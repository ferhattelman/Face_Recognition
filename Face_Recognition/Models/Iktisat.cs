using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Face_Recognition.Models
{
    [Table("Iktisat")]
    public class Iktisat
    {
        [Key]
        public int Id { get; set; }
        public string Name_Surname { get; set; }
        public string Image { get; set; }
    }
}
