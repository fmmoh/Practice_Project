using BookForm.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookForm.Models
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name of the book is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public List<int> Writers { get; set; }

        [Required(ErrorMessage = "Language of the book is required.")]

        public string Language { get; set; }

        public DateTime ReleaseImage { get; set; }

        public string CoverImage { get; set; }

    }
}
