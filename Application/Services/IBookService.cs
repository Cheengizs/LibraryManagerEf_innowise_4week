using Application.Common;
using Application.Dto_s;

namespace Application.Services;

public interface IBookService
{
    Task<ServiceResult<List<BookResponse>>> GetAllBooksAsync();
    Task<ServiceResult<BookResponse>> GetBookByIdAsync(int id);
    Task<ServiceResult<List<BookResponse>>> GetBooksByAuthorIdAsync(int authorId);
    Task<ServiceResult<List<BookResponse>>> GetBooksAfterAsync(int year);
    Task<ServiceResult<BookResponse>> CreateBookAsync(BookRequest bookRequest);
    Task<ServiceResult<BookResponse>> UpdateBookAsync(BookRequest bookRequest);
    Task<ServiceResult> DeleteBookByIdAsync(int id);
}