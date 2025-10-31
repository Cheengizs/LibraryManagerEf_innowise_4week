using Domain.Models;

namespace Application.RepositoriesInterfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<List<Book>> GetBooksAfterYearAsync(int year);
    Task<Book> AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
    
}