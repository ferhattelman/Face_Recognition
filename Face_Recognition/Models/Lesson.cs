using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Face_Recognition.Models
{
    [Table("Lesson")]
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
