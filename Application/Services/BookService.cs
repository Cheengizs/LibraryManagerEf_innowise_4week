using Application.Commons;
using Application.Dto_s.Book;
using Application.RepositoriesInterfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<BookRequest> _validator;

    public BookService(IBookRepository bookRepository, IMapper mapper, IValidator<BookRequest> validator, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _validator = validator;
        _authorRepository = authorRepository;
    }

    public async Task<ServiceResult<List<BookResponse>>> GetAllBooksAsync()
    {
        List<Book> booksFromRepo = await _bookRepository.GetAllBooksAsync();
        List<BookResponse> booksResult = _mapper.Map<List<BookResponse>>(booksFromRepo);
        ServiceResult<List<BookResponse>> result = ServiceResult<List<BookResponse>>.Success(booksResult);
        return result;
    }

    public async Task<ServiceResult<BookResponse>> GetBookByIdAsync(int id)
    {
        Book? bookFromRepo = await _bookRepository.GetBookByIdAsync(id);
        if (bookFromRepo is null)
        {
            ServiceResult<BookResponse> failResult = ServiceResult<BookResponse>.Failure(["Book not found"], ServiceErrorCode.NotFound);
        }
        
        BookResponse bookResult = _mapper.Map<BookResponse>(bookFromRepo);
        ServiceResult<BookResponse> result = ServiceResult<BookResponse>.Success(bookResult);
        return result;
    }

    public async Task<ServiceResult<List<BookResponse>>> GetBooksByAuthorIdAsync(int authorId)
    {
        List<Book> booksFromRepo = await _bookRepository.GetBooksByAuthorIdAsync(authorId);
        List<BookResponse> bookResult = _mapper.Map<List<BookResponse>>(booksFromRepo);
        ServiceResult<List<BookResponse>> result = ServiceResult<List<BookResponse>>.Success(bookResult); 
        return result;
    }

    public async Task<ServiceResult<List<BookResponse>>> GetBooksAfterAsync(int year)
    {
        List<Book> booksFromRepo = await _bookRepository.GetBooksAfterYearAsync(year);
        List<BookResponse> bookResult = _mapper.Map<List<BookResponse>>(booksFromRepo);
        ServiceResult<List<BookResponse>> result = ServiceResult<List<BookResponse>>.Success(bookResult);
        return result;
    }

    public async Task<ServiceResult<BookResponse>> CreateBookAsync(BookRequest bookRequest)
    {
        bool isAuthorExists = (await _authorRepository.GetAuthorByIdAsync(bookRequest.AuthorId)) != null;
        if (!isAuthorExists)
        {
            ServiceResult<BookResponse> failResult = ServiceResult<BookResponse>.Failure(["Author not found"], ServiceErrorCode.NotFound);
            return failResult;
        }
        
        ValidationResult validationResult = await _validator.ValidateAsync(bookRequest);
        if (!validationResult.IsValid)
        {
            ServiceResult<BookResponse> failResult =
                ServiceResult<BookResponse>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ServiceErrorCode.Validation);
            return failResult;
        }

        Book book = _mapper.Map<Book>(bookRequest);

        Book responseFromRepo = await _bookRepository.AddBookAsync(book);
        BookResponse bookResponse = _mapper.Map<BookResponse>(responseFromRepo);
        
        ServiceResult<BookResponse> result = ServiceResult<BookResponse>.Success(bookResponse);
        return result;
    }

    public async Task<ServiceResult<BookResponse>> UpdateBookAsync(int id, BookRequest bookRequest)
    {
        bool isBookExists = (await _bookRepository.GetBookByIdAsync(id)) != null;
        if (!isBookExists)
        {
            ServiceResult<BookResponse> failResult =
                ServiceResult<BookResponse>.Failure(["Book not found"], ServiceErrorCode.NotFound);
        }
        
        ValidationResult validationResult = await _validator.ValidateAsync(bookRequest);
        if (!validationResult.IsValid) 
        {
            ServiceResult<BookResponse> failResult   =
                ServiceResult<BookResponse>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ServiceErrorCode.Validation);
            return failResult;
        }

        Book book = _mapper.Map<Book>(bookRequest);
        book.Id = id;
        await _bookRepository.UpdateBookAsync(book);
        
        BookResponse bookResponse = _mapper.Map<BookResponse>(book);
        ServiceResult<BookResponse> result = ServiceResult<BookResponse>.Success(bookResponse);
        return result;
    }

    public async Task<ServiceResult> DeleteBookByIdAsync(int id)
    {
        var bookFromRepo = await _bookRepository.GetBookByIdAsync(id);
        if (bookFromRepo is null)
        {
            return ServiceResult.Failure(["Not found"], ServiceErrorCode.NotFound);
        }
        
        await _bookRepository.DeleteBookAsync(id);
        ServiceResult result = ServiceResult.Success();
        return result;
    }
}