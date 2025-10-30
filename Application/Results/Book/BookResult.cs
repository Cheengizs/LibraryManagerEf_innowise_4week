namespace Application.Results.Book;

public class BookResult
{
    public bool Success { get; set; }
    public Dictionary<string, string[]> Messages { get; set; }
}