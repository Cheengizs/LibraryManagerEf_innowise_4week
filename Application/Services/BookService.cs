using Application.Common;
using Application.Dto_s;
using Application.RepositoriesInterfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<BookRequest> _validator;

    public BookService(IBookRepository repository, IMapper mapper, IValidator<BookRequest> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ServiceResult<List<BookResponse>>> GetAllBooksAsync()
    {
        List<Book> booksFromRepo = await _repository.GetAllBooksAsync();
        List<BookResponse> booksResult = _mapper.Map<List<BookResponse>>(booksFromRepo);
        ServiceResult<List<BookResponse>> result = ServiceResult<List<BookResponse>>.Success(booksResult);
        return result;
    }

    public async Task<ServiceResult<BookResponse>> GetBookByIdAsync(int id)
    {
        Book? bookFromRepo = await _repository.GetBookByIdAsync(id);
        if (bookFromRepo == null)
        {
            ServiceResult<BookResponse> failResult = ServiceResult<BookResponse>.Failure(["Book not found"]);
        }
        
        BookResponse bookResult = _mapper.Map<BookResponse>(bookFromRepo);
        ServiceResult<BookResponse> result = ServiceResult<BookResponse>.Success(bookResult);
        return result;
    }

    public async Task<ServiceResult<List<BookResponse>>> GetBooksByAuthorIdAsync(int authorId)
    {
        List<Book> booksFromRepo = await _repository.GetBooksByAuthorIdAsync(authorId);
        List<BookResponse> bookResult = _mapper.Map<List<BookResponse>>(booksFromRepo);
        ServiceResult<List<BookResponse>> result = ServiceResult<List<BookResponse>>.Success(bookResult); 
        return result;
    }

    public async Task<ServiceResult<List<BookResponse>>> GetBooksAfterAsync(int year)
    {
        List<Book> booksFromRepo = await _repository.GetBooksAfterYearAsync(year);
        List<BookResponse> bookResult = _mapper.Map<List<BookResponse>>(booksFromRepo);
        ServiceResult<List<BookResponse>> result = ServiceResult<List<BookResponse>>.Success(bookResult);
        return result;
    }

    public async Task<ServiceResult<BookResponse>> CreateBookAsync(BookRequest bookRequest)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(bookRequest);
        if (!validationResult.IsValid)
        {
            ServiceResult<BookResponse> res =
                ServiceResult<BookResponse>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            return res;
        }

        Book book = _mapper.Map<Book>(bookRequest);

        await _repository.AddBookAsync(book);
        BookResponse bookResponse = _mapper.Map<BookResponse>(book);
        
        ServiceResult<BookResponse> result = ServiceResult<BookResponse>.Success(bookResponse);
        return result;
    }

    public async Task<ServiceResult<BookResponse>> UpdateBookAsync(BookRequest bookRequest)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(bookRequest);
        if (!validationResult.IsValid) 
        {
            ServiceResult<BookResponse> res =
                ServiceResult<BookResponse>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            return res;
        }

        Book book = _mapper.Map<Book>(bookRequest);
        await _repository.UpdateBookAsync(book);
        
        BookResponse bookResponse = _mapper.Map<BookResponse>(book);
        ServiceResult<BookResponse> result = ServiceResult<BookResponse>.Success(bookResponse);
        return result;
    }

    public async Task<ServiceResult> DeleteBookByIdAsync(int id)
    {
        var bookFromRepo = await _repository.GetBookByIdAsync(id);
        if (bookFromRepo == null)
        {
            return ServiceResult.Failure(["Not found"]);
        }
        
        await _repository.DeleteBookAsync(id);
        ServiceResult result = ServiceResult.Success();
        return result;
    }
}