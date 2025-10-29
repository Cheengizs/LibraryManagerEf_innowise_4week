using Application.RepositoriesInterfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DatabaseContexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public BookRepository(IMapper mapper, LibraryDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<List<Book>> GetAllBooksAsync()
    {
        List<BookEntity> booksFromDb = await _context.Books.ToListAsync();
        List<Book> result = _mapper.Map<List<Book>>(booksFromDb);
        return result;
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        BookEntity? bookFromDb = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if(bookFromDb == null)
            return null;
        
        Book result = _mapper.Map<Book>(bookFromDb);
        return result;
    }

    public async Task<List<Book>> GetBooksByAuthorIdAsync(int authorId)
    {
        List<BookEntity> booksFromDb = await _context.Books.Where(b => b.AuthorId == authorId).ToListAsync();
        List<Book> result = _mapper.Map<List<Book>>(booksFromDb);
        return result;
    }

    public async Task<List<Book>> GetBooksAfterYearAsync(int year)
    {
        List<BookEntity> booksFromDb = await _context.Books.Where(b => b.PublishedYear >= year).ToListAsync();
        List<Book> result = _mapper.Map<List<Book>>(booksFromDb);
        return result;
    }

    public async Task AddBookAsync(Book book)
    {
        BookEntity bookToAdd = _mapper.Map<BookEntity>(book);
        await _context.Books.AddAsync(bookToAdd);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        BookEntity? bookFromDb = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
        if(bookFromDb == null)
            return;
        
        bookFromDb.Title = book.Title;
        bookFromDb.AuthorId = book.AuthorId;
        bookFromDb.PublishedYear = book.PublishedYear;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        BookEntity? bookFromDb = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if(bookFromDb == null)
            return;
        
        _context.Books.Remove(bookFromDb);
        await _context.SaveChangesAsync();
    }
}