using System.ComponentModel.DataAnnotations;

namespace BookForm.Entities
{
    public class Writer
    {
        public int  Id { get; set;}
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<Book> Books { get; set; }

        public DateTime Published { get; internal set; } = DateTime.Now;

        public DateTime? ModeifiedDate { get; set; } 




    }
}
