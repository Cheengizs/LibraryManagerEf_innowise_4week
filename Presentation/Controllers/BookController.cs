using Application.Commons;
using Application.Dto_s.Book;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerEf.Controllers;

[ApiController]
[Route("api/v1/books")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookResponse>>> GetAllBooksAsync()
    {
        ServiceResult<List<BookResponse>> serviceResult = await _bookService.GetAllBooksAsync();
        if (!serviceResult.IsSuccess)
        {
            return BadRequest(serviceResult.Errors);
        }

        List<BookResponse> books = serviceResult.Value;
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookResponse>> GetBookByIdAsync(int id)
    {
        ServiceResult<BookResponse> serviceResult = await _bookService.GetBookByIdAsync(id);
        if (!serviceResult.IsSuccess)
        {
            if (serviceResult.ErrorCode == ServiceErrorCode.NotFound)
                return NotFound("Book not found");

            return BadRequest(serviceResult.Errors);
        }

        BookResponse book = serviceResult.Value;
        return Ok(book);
    }

    [HttpGet("byAuthor/{authorId:int}")]
    public async Task<ActionResult<List<BookResponse>>> GetBooksByAuthorIdAsync(int authorId)
    {
        ServiceResult<List<BookResponse>> serviceResult = await _bookService.GetBooksByAuthorIdAsync(authorId);
        if (!serviceResult.IsSuccess)
        {
            if (serviceResult.ErrorCode == ServiceErrorCode.NotFound)
                return NotFound("Book not found");

            return BadRequest(serviceResult.Errors);
        }

        List<BookResponse> books = serviceResult.Value;
        return Ok(books);
    }

    [HttpGet("getAfter/{year:int}")]
    public async Task<ActionResult<List<BookResponse>>> GetBooksAfterAsync(int year)
    {
        ServiceResult<List<BookResponse>> serviceResult = await _bookService.GetBooksAfterAsync(year);
        if (!serviceResult.IsSuccess)
        {
            return BadRequest(serviceResult.Errors);
        }

        List<BookResponse> books = serviceResult.Value;
        return Ok(books);
    }

    [HttpPost("createBook")]
    public async Task<ActionResult<BookResponse>> CreateBookAsync([FromBody] BookRequest bookRequest)
    {
        ServiceResult<BookResponse> serviceResult = await _bookService.CreateBookAsync(bookRequest);
        if (!serviceResult.IsSuccess)
        {
            if(serviceResult.ErrorCode == ServiceErrorCode.NotFound)
                return NotFound("Book not found");
            
            if(serviceResult.ErrorCode == ServiceErrorCode.Validation)
                return BadRequest(serviceResult.Errors);
            
            return BadRequest(serviceResult.Errors);
        }

        BookResponse bookResult = serviceResult.Value;
        var location = $"{Request.Scheme}://{Request.Host}/api/v1/books/{bookResult.Id}";
        return Created(location, bookResult);
    }

    [HttpPut("{bookId:int}")]
    public async Task<ActionResult<BookResponse>> UpdateBookAsync(int bookId,
        [FromBody] BookRequest bookRequest)
    {
        ServiceResult<BookResponse> serviceResult = await _bookService.UpdateBookAsync(bookId, bookRequest);
        if (!serviceResult.IsSuccess)
        {
            if (serviceResult.ErrorCode == ServiceErrorCode.NotFound)
                return NotFound(serviceResult.Errors);
            
            if (serviceResult.ErrorCode == ServiceErrorCode.Validation)
                return BadRequest(serviceResult.Errors);
            
            return BadRequest(serviceResult.Errors);
        }

        BookResponse bookResult = serviceResult.Value;
        return Ok(bookResult);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteBookAsync(int id)
    {
        ServiceResult serviceResult = await _bookService.DeleteBookByIdAsync(id);
        if (!serviceResult.IsSuccess)
        {
            if(serviceResult.ErrorCode == ServiceErrorCode.NotFound)
                return NotFound(serviceResult.Errors);
            
            return BadRequest(serviceResult.Errors);
        }

        return NoContent();
    }
}