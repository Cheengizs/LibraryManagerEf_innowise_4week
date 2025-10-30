using Application.Dto_s.Author;

namespace Application.Services;

public interface IAuthorService
{
    Task<ServiceResult<List<AuthorResponse>>> GetAllAuthorsAsync();
    Task<ServiceResult<AuthorResponse>> GetAuthorByIdAsync(int id);
    Task<ServiceResult<List<AuthorResponse>>> GetAuthorByNameAsync(string name);
    Task<ServiceResult<List<AuthorResponse>>> GetAuthorsWithBooksMoreThanAsync(int booksCount);
    Task<ServiceResult<AuthorResponse>> AddAuthorAsync(AuthorRequest authorRequest);
    Task<ServiceResult<AuthorResponse>> UpdateAuthorAsync(int id, AuthorRequest authorRequest);
    Task<ServiceResult> DeleteAuthorAsync(int authorId);
}