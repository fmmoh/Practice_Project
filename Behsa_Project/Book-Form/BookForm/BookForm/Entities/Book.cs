namespace BookForm.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<Writer> Writers { get; set; }

        public string Language { get; set; }

        public DateTime ReleaseImage { get; set; }

        public required string CoverImage { get; set; }

        public DateTime Published { get; internal set; } = DateTime.Now;

        public DateTime? ModeifiedDate { get; set; }



    }
}
