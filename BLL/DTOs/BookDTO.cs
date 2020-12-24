namespace BLL.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? AuthorId { get; set; }
        public AuthorDTO Author { get; set; }
        public int? GenreId { get; set; }
        public GenreDTO Genre { get; set; }
        public int Quantity { get; set; }
    }
}