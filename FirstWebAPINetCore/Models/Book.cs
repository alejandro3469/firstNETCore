namespace FirstWebAPINetCore.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Synopsis { get; set; }
        public string Image { get; set; }
        public Format Format { get; set; }
        public Language Language { get; set; }
        public double Price { get; set; }
        public int GenreId { get; set; }
        public int AuthorID { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; }
    }
    public enum Format
    {
        Paperback,
        HardCover,
        Kindle,
        PDF
    }
    public enum Language
    {
        English,
        Spanish,
        French
    }
}
