using Application.Filters;
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

    public async Task<List<Author>> GetAllAuthorsAsync(AuthorFilter? filter = null)
    {
        var query = _context.Authors.AsQueryable();

        if (filter is not null)
        {
            if (filter.MinimumBooksAmount.HasValue)
            {
                query = query.Where(a => a.Books.Count() >= filter.MinimumBooksAmount.Value);
            }

            if (filter.MaximumBooksAmount.HasValue)
            {
                query = query.Where(a => a.Books.Count() <= filter.MaximumBooksAmount.Value);
            }
        }

        var authorsFromDb = await query.ToListAsync();
        return _mapper.Map<List<Author>>(authorsFromDb);
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        AuthorEntity? authorFromDb = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (authorFromDb is null)
            return null;

        Author result = _mapper.Map<Author>(authorFromDb);
        return result;
    }

    public async Task<List<Author>> GetAuthorByNameAsync(string name)
    {
        List<AuthorEntity> authorsFromDb = await _context.Authors
            .Where(a => a.Name.Trim().ToLower().Contains(name.Trim().ToLower())).ToListAsync();
        List<Author> result = _mapper.Map<List<Author>>(authorsFromDb);
        return result;
    }

    public async Task<Author> AddAuthorAsync(Author author)
    {
        AuthorEntity authorToAdd = _mapper.Map<AuthorEntity>(author);
        await _context.Authors.AddAsync(authorToAdd);
        await _context.SaveChangesAsync();
        Author result = _mapper.Map<Author>(authorToAdd);
        return result;
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        AuthorEntity? authorFromDb = await _context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
        if (authorFromDb is null)
            return;

        authorFromDb.Name = author.Name;
        authorFromDb.DateOfBirth = author.DateOfBirth;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAuthorAsync(int id)
    {
        AuthorEntity? authorFromDb = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (authorFromDb is null)
            return;

        _context.Authors.Remove(authorFromDb);
        await _context.SaveChangesAsync();
    }
}