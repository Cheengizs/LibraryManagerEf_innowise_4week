using Application.Dto_s.Author;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerEf.Controllers;

[ApiController]
[Route("api/v1/authors")]
public class AuthorController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AuthorResponse>>> GetAllAuthorsAsync()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorResponse>> GetAuthorByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("by-name/{name}")]
    public async Task<ActionResult<List<AuthorResponse>>> GetAuthorByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    [HttpGet("with-books-more-than/{count:int}")]
    public async Task<ActionResult<List<AuthorResponse>>> GetAuthorsWithBooksMoreThanAsync(int count)
    {
        throw new NotImplementedException();   
    }

    [HttpPost]
    public async Task<ActionResult<AuthorResponse>> CreateAuthorAsync([FromBody] AuthorRequest authorRequest)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{authorId:int}")]
    public async Task<ActionResult<AuthorResponse>> UpdateAuthorAsync(int authorId,
        [FromBody] AuthorRequest authorRequest)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{authorId:int}")]
    public async Task<ActionResult> DeleteAuthorAsync(int authorId)
    {
        throw new NotImplementedException();
    }
}