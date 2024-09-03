using System.ComponentModel.DataAnnotations;

namespace BookForm.Models
{
    public class WriterViewModel
    {
        public int Id { get; set; }
 
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
