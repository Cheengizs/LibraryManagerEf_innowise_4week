using Application.Filters;
using Domain.Models;

namespace Application.RepositoriesInterfaces;

public interface IAuthorRepository
{
    Task<List<Author>> GetAllAuthorsAsync(AuthorFilter? filter = null);
    Task<Author?> GetAuthorByIdAsync(int id);
    Task<List<Author>> GetAuthorByNameAsync(string name);
    Task<Author> AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(int id);
}