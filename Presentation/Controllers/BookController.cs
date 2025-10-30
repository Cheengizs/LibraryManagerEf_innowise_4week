using Application.Common;
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
    
    [HttpGet("all")]
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
            return NotFound("Book not found");
        }
        
        BookResponse book = serviceResult.Value;
        return Ok(book);
    }

    [HttpGet("author/{authorId:int}")]
    public async Task<ActionResult<List<BookResponse>>> GetBooksByAuthorIdAsync(int authorId)
    {
        ServiceResult<List<BookResponse>> serviceResult = await _bookService.GetBooksByAuthorIdAsync(authorId);
        if (!serviceResult.IsSuccess)
        {
            return BadRequest(serviceResult.Errors);
        }
        
        List<BookResponse> books = serviceResult.Value;
        return Ok(books);
    }
}