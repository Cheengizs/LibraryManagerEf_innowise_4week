using Domain.Models;

namespace Application.RepositoriesInterfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<List<Book>> GetBooksByAuthorIdAsync(int authorId);
    Task<List<Book>> GetBooksAfterYearAsync(int year);
    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
    
}