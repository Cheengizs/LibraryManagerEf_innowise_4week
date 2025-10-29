namespace Infrastructure.Entities;

public class AuthorEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    
    public List<BookEntity> Books { get; set; } 
}