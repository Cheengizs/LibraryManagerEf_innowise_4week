using Application.Commons;
using Application.Dto_s.Author;
using Application.Dto_s.Book;
using Application.Filters;
using Application.RepositoriesInterfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<AuthorRequest> _validator;

    public AuthorService(IAuthorRepository repository, IMapper mapper, IValidator<AuthorRequest> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ServiceResult<List<AuthorResponse>>> GetAllAuthorsAsync(AuthorFilter? filter = null)
    {
        List<Author> authorsFromRepo = await _repository.GetAllAuthorsAsync(filter);
        List<AuthorResponse> authorsResult = _mapper.Map<List<AuthorResponse>>(authorsFromRepo);
        ServiceResult<List<AuthorResponse>> result = ServiceResult<List<AuthorResponse>>.Success(authorsResult);
        return result;
    }

    public async Task<ServiceResult<AuthorResponse>> GetAuthorByIdAsync(int id)
    {
        Author? authorFromRepo = await _repository.GetAuthorByIdAsync(id);
        if (authorFromRepo is null)
        {
            ServiceResult<AuthorResponse> failResult = ServiceResult<AuthorResponse>.Failure(["Book not found"], ServiceErrorCode.NotFound);
            return failResult;
        }
        
        AuthorResponse authorResult = _mapper.Map<AuthorResponse>(authorFromRepo);
        ServiceResult<AuthorResponse> result = ServiceResult<AuthorResponse>.Success(authorResult);
        return result;
    }

    public async Task<ServiceResult<List<AuthorResponse>>> GetAuthorByNameAsync(string name)
    {
        List<Author> authorsFromRepo = await _repository.GetAuthorByNameAsync(name);
        List<AuthorResponse> authorsResult = _mapper.Map<List<AuthorResponse>>(authorsFromRepo);
        ServiceResult<List<AuthorResponse>> result = ServiceResult<List<AuthorResponse>>.Success(authorsResult);
        return result;
    }

    public async Task<ServiceResult<List<BookResponse>>> GetAuthorIdBooksAsync(int year)
    {
        List<Book> booksFromRepo = await _repository.GetAuthorIdBooksAsync(year);
        List<BookResponse> booksResult = _mapper.Map<List<BookResponse>>(booksFromRepo);
        ServiceResult<List<BookResponse>> result = ServiceResult<List<BookResponse>>.Success(booksResult);
        return result;
    }

    public async Task<ServiceResult<AuthorResponse>> AddAuthorAsync(AuthorRequest authorRequest)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(authorRequest);
        if (!validationResult.IsValid)
        {
            ServiceResult<AuthorResponse> failResult =
                ServiceResult<AuthorResponse>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ServiceErrorCode.Validation);
            return failResult;
        }
        
        Author author = _mapper.Map<Author>(authorRequest);
        
        var responseFromRepo = await _repository.AddAuthorAsync(author);
        AuthorResponse authorResult = _mapper.Map<AuthorResponse>(responseFromRepo);
        
        ServiceResult<AuthorResponse> result = ServiceResult<AuthorResponse>.Success(authorResult);
        return result;
    }

    public async Task<ServiceResult<AuthorResponse>> UpdateAuthorAsync(int id, AuthorRequest authorRequest)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(authorRequest);
        if (!validationResult.IsValid)
        {
            ServiceResult<AuthorResponse> failResult = ServiceResult<AuthorResponse>.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ServiceErrorCode.Validation);
            return failResult;
        }
        
        Author author = _mapper.Map<Author>(authorRequest);
        author.Id = id;
        await _repository.UpdateAuthorAsync(author);
        AuthorResponse authorResult = _mapper.Map<AuthorResponse>(author);
        ServiceResult<AuthorResponse> result = ServiceResult<AuthorResponse>.Success(authorResult);
        return result;
    }

    public async Task<ServiceResult> DeleteAuthorAsync(int authorId)
    {
        var author = await _repository.GetAuthorByIdAsync(authorId);
        if (author is null)
        {
            ServiceResult failResult = ServiceResult.Failure(["Author not found"], ServiceErrorCode.NotFound);
            return failResult;
        }
        
        await _repository.DeleteAuthorAsync(authorId);
        ServiceResult result = ServiceResult.Success();
        return result;
    }
}