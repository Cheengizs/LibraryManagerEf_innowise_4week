using Application.Commons;
using Application.Dto_s.Author;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerEf.Controllers;

[ApiController]
[Route("api/v1/authors")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuthorResponse>>> GetAllAuthorsAsync()
    {
        ServiceResult<List<AuthorResponse>> serviceResult = await _authorService.GetAllAuthorsAsync();
        if (!serviceResult.IsSuccess)
        {
            return BadRequest(serviceResult.Errors);
        }

        List<AuthorResponse> authors = serviceResult.Value;
        return Ok(authors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorResponse>> GetAuthorByIdAsync(int id)
    {
        ServiceResult<AuthorResponse> serviceResult = await _authorService.GetAuthorByIdAsync(id);
        if (!serviceResult.IsSuccess)
        {
            if (serviceResult.ErrorCode == ServiceErrorCode.NotFound)
                return NotFound("Author not found");

            return BadRequest(serviceResult.Errors);
        }

        AuthorResponse author = serviceResult.Value;
        return Ok(author);
    }

    [HttpGet("by-name/{name}")]
    public async Task<ActionResult<List<AuthorResponse>>> GetAuthorByNameAsync(string name)
    {
        ServiceResult<List<AuthorResponse>> serviceResult = await _authorService.GetAuthorByNameAsync(name);
        if (!serviceResult.IsSuccess)
        {
            return BadRequest(serviceResult.Errors);
        }

        List<AuthorResponse> authors = serviceResult.Value;
        return Ok(authors);
    }

    [HttpGet("with-books-more-than/{count:int}")]
    public async Task<ActionResult<List<AuthorResponse>>> GetAuthorsWithBooksMoreThanAsync(int count)
    {
        ServiceResult<List<AuthorResponse>>
            serviceResult = await _authorService.GetAuthorsWithBooksMoreThanAsync(count);
        if (!serviceResult.IsSuccess)
        {
            return BadRequest(serviceResult.Errors);
        }

        List<AuthorResponse> authors = serviceResult.Value;
        return Ok(authors);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorResponse>> CreateAuthorAsync([FromBody] AuthorRequest authorRequest)
    {
        ServiceResult<AuthorResponse> serviceResult = await _authorService.AddAuthorAsync(authorRequest);
        if (!serviceResult.IsSuccess)
        {
            if (serviceResult.ErrorCode == ServiceErrorCode.Validation)
                return BadRequest(serviceResult.Errors);
            
            return BadRequest(serviceResult.Errors);
        }
        AuthorResponse author = serviceResult.Value;
        
        return CreatedAtAction(
            "GetAuthorById",
            new { id = author.Id },
            author
        );
    }

    [HttpPut("{authorId:int}")]
    public async Task<ActionResult<AuthorResponse>> UpdateAuthorAsync(int authorId,
        [FromBody] AuthorRequest authorRequest)
    {
        ServiceResult<AuthorResponse> serviceResult = await _authorService.UpdateAuthorAsync(authorId, authorRequest);
        if (!serviceResult.IsSuccess)
        {
            if(serviceResult.ErrorCode == ServiceErrorCode.NotFound)
                return NotFound("Author not found");
            
            if(serviceResult.ErrorCode == ServiceErrorCode.Validation)
                return BadRequest(serviceResult.Errors);
            
            return BadRequest(serviceResult.Errors);
        }
        
        AuthorResponse author = serviceResult.Value;
        return Ok(author);
    }

    [HttpDelete("{authorId:int}")]
    public async Task<ActionResult> DeleteAuthorAsync(int authorId)
    {
        ServiceResult serviceResult = await _authorService.DeleteAuthorAsync(authorId);
        if (!serviceResult.IsSuccess)
        {
            if(serviceResult.ErrorCode == ServiceErrorCode.NotFound)
                return NotFound("Author not found");

            return BadRequest(serviceResult.Errors);
        }

        return NoContent();
    }
}