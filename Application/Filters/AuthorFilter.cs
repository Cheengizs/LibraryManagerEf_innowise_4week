namespace Application.Filters;

public class AuthorFilter
{
    public int? MinimumBooksAmount { get; set; }
    public int? MaximumBooksAmount { get; set; }

    public AuthorFilter()
    {
        // exception throws without parameterless constructor((
    }
    
    public AuthorFilter(int? minimumBooksAmount, int? maximumBooksAmount)
    {
        MinimumBooksAmount = minimumBooksAmount;
        MaximumBooksAmount = maximumBooksAmount;
    }
}