using System.ComponentModel.DataAnnotations;

namespace BookForm.Models
{
    public class BookListViewModel
    {

        public int Id { get; set; }
        
        public string Title { get; set; }
        // List of writers
        public List<WriterViewModel> Writers { get; set; }

        public string Language { get; set; }

        public DateTime ReleaseImage { get; set; }

        public string CoverImage { get; set; }


    }
}
