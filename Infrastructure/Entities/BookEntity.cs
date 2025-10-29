namespace Infrastructure.Entities;

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; }    
    public int PublishedYear { get; set; }
    
    public int AuthorId { get; set; }
    
    public AuthorEntity Author { get; set; }
}