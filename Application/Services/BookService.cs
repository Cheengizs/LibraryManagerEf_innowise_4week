using Application.Dto_s;
using Application.RepositoriesInterfaces;
using Application.Results.Book;
using Application.Validators;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services;

public class BookService
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

    public async Task<List<BookResponse>> GetAllBooksAsync()
    {
        List<Book> booksFromRepo = await _repository.GetAllBooksAsync();
        List<BookResponse> result = _mapper.Map<List<BookResponse>>(booksFromRepo);
        return result;
    }

    public async Task<BookResponse?> GetBookByIdAsync(int id)
    {
        Book? bookFromRepo = await _repository.GetBookByIdAsync(id);
        if(bookFromRepo == null)
            return null;
        
        BookResponse result = _mapper.Map<BookResponse>(bookFromRepo);
        return result;
    }

    public async Task<List<BookResponse>> GetBooksByAuthorIdAsync(int authorId)
    {
        List<Book> booksFromRepo = await _repository.GetBooksByAuthorIdAsync(authorId);
        List<BookResponse> result = _mapper.Map<List<BookResponse>>(booksFromRepo);
        return result;
    }

    public async Task<List<BookResponse>> GetBooksAfterAsync(int year)
    {
        List<Book> booksFromRepo = await _repository.GetBooksAfterYearAsync(year);
        List<BookResponse> result = _mapper.Map<List<BookResponse>>(booksFromRepo);
        return result;
    }

    public async Task<BookResult> CreateBookAsync(BookRequest bookRequest)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(bookRequest);
        if (!validationResult.IsValid)
        {
            return new BookResult { Success = false, Messages = validationResult.ToDictionary().ToDictionary() };
        }
        
        Book book = _mapper.Map<Book>(bookRequest);
        
        await _repository.AddBookAsync(book);
        
        BookResult result = new BookResult { Success = true };
        return result;
    }

    public async Task<BookResult> UpdateBookAsync(BookRequest bookRequest)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(bookRequest);

        if (!validationResult.IsValid)
        {
            return new BookResult { Success = false, Messages = validationResult.ToDictionary().ToDictionary() };
        }
        Book book = _mapper.Map<Book>(bookRequest);
        await _repository.UpdateBookAsync(book);
        BookResult result = new BookResult { Success = true };
        return result;
    }

    public async Task<BookResult> DeleteBookByIdAsync(int id)
    {
        Book? bookFromRepo = await _repository.GetBookByIdAsync(id);
        
        if (bookFromRepo == null)
        {
            return new BookResult(){Success = false, Messages = new Dictionary<string, string[]>() };
        }
        await _repository.DeleteBookAsync(id);
        BookResult result = new BookResult { Success = true };
        return result;
    }
}