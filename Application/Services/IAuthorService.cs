using Application.Dto_s.Author;
using Application.Dto_s.Book;
using Application.Filters;

namespace Application.Services;

public interface IAuthorService
{
    Task<ServiceResult<List<AuthorResponse>>> GetAllAuthorsAsync(AuthorFilter? filter = null);
    Task<ServiceResult<AuthorResponse>> GetAuthorByIdAsync(int id);
    Task<ServiceResult<List<AuthorResponse>>> GetAuthorByNameAsync(string name);
    Task<ServiceResult<List<BookResponse>>> GetAuthorIdBooksAsync(int year);
    Task<ServiceResult<AuthorResponse>> AddAuthorAsync(AuthorRequest authorRequest);
    Task<ServiceResult<AuthorResponse>> UpdateAuthorAsync(int id, AuthorRequest authorRequest);
    Task<ServiceResult> DeleteAuthorAsync(int authorId);
}