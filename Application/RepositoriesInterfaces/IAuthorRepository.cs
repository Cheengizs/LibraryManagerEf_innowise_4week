using Domain.Models;

namespace Application.RepositoriesInterfaces;

public interface IAuthorRepository
{
    Task<List<Author>> GetAllAuthorsAsync();
    Task<Author?> GetAuthorByIdAsync(int id);
    Task<List<Author>> GetAuthorByNameAsync(string name);
    Task<List<Author>> GetAuthorsWithBooksMoreThanAsync(int booksCount);
    Task AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(int id);
}