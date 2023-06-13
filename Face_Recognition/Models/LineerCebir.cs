using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Face_Recognition.Models
{
    [Table("LineerCebir")]
    public class LineerCebir
    {
        [Key]
        public int Id { get; set; }
        public string Name_Surname { get; set; }
        public string Image { get; set; }
    }
}
