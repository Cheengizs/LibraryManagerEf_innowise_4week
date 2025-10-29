using Application.RepositoriesInterfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DatabaseContexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public AuthorRepository(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Author>> GetAllAuthorsAsync()
    {
        List<AuthorEntity> authorsFromDb = await _context.Authors.ToListAsync();
        List<Author> result = _mapper.Map<List<Author>>(authorsFromDb);
        return result;
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        AuthorEntity? authorFromDb = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if(authorFromDb == null)
            return null;
        
        Author result = _mapper.Map<Author>(authorFromDb);
        return result;
    }

    public async Task<List<Author>> GetAuthorByNameAsync(string name)
    {
        List<AuthorEntity> authorsFromDb = await _context.Authors.Where(a => a.Name.Contains(name)).ToListAsync();
        List<Author> result = _mapper.Map<List<Author>>(authorsFromDb);
        return result;
    }

    public async Task<List<Author>> GetAuthorsWithBooksMoreThanAsync(int booksCount)
    {
        List<AuthorEntity> authorsFromDb = await _context.Authors.Where(a => a.Books.Count >= booksCount).ToListAsync();
        List<Author> result = _mapper.Map<List<Author>>(authorsFromDb);
        return result;
    }

    public async Task AddAuthorAsync(Author author)
    {
        AuthorEntity authorToAdd = _mapper.Map<AuthorEntity>(author);
        await _context.Authors.AddAsync(authorToAdd);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        AuthorEntity? authorFromDb = await _context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
        if (authorFromDb == null)
            return;
        
        authorFromDb.Name = author.Name;
        authorFromDb.DateOfBirth = author.DateOfBirth;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAuthorAsync(int id)
    {
        AuthorEntity? authorFromDb = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (authorFromDb == null)
            return;
        
        _context.Authors.Remove(authorFromDb);
        await _context.SaveChangesAsync();
    }
}