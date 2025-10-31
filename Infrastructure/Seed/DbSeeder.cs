using Infrastructure.DatabaseContexts;
using Infrastructure.Entities;

namespace Infrastructure.Seed;

public class DbSeeder
{
    private readonly LibraryDbContext _context;

    public DbSeeder(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await SeedAuthors();
        await SeedBooks();
    }

    public async Task SeedAuthors()
    {
        await _context.Database.EnsureCreatedAsync();

        if (!_context.Authors.Any())
        {
            var authors = new List<AuthorEntity>
            {
                new AuthorEntity { Name = "Александр Пушкин", DateOfBirth = new DateOnly(1799, 6, 6) },
                new AuthorEntity { Name = "Фёдор Достоевский", DateOfBirth = new DateOnly(1821, 11, 11) },
                new AuthorEntity { Name = "Лев Толстой", DateOfBirth = new DateOnly(1828, 9, 9) },
                new AuthorEntity { Name = "Анна Ахматова", DateOfBirth = new DateOnly(1889, 6, 23) },
                new AuthorEntity { Name = "Сергей Есенин", DateOfBirth = new DateOnly(1895, 10, 3) }
            };

            await _context.Authors.AddRangeAsync(authors);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SeedBooks()
    {
        await _context.Database.EnsureCreatedAsync();
        
        if (!_context.Books.Any())
        {
            var pushkin = _context.Authors.First(a => a.Name == "Александр Пушкин");
            var dostoevsky = _context.Authors.First(a => a.Name == "Фёдор Достоевский");
            var tolstoy = _context.Authors.First(a => a.Name == "Лев Толстой");
            var akhmatova = _context.Authors.First(a => a.Name == "Анна Ахматова");
            var yesenin = _context.Authors.First(a => a.Name == "Сергей Есенин");

            var books = new List<BookEntity>
            {
                new BookEntity { Title = "Евгений Онегин", AuthorId = pushkin.Id, PublishedYear = 1833 },
                new BookEntity { Title = "Руслан и Людмила", AuthorId = pushkin.Id, PublishedYear = 1820 },

                new BookEntity { Title = "Преступление и наказание", AuthorId = dostoevsky.Id, PublishedYear = 1866 },
                new BookEntity { Title = "Идиот", AuthorId = dostoevsky.Id, PublishedYear = 1869 },

                new BookEntity { Title = "Война и мир", AuthorId = tolstoy.Id, PublishedYear = 1869 },
                new BookEntity { Title = "Анна Каренина", AuthorId = tolstoy.Id, PublishedYear = 1877 },

                new BookEntity { Title = "Реквием", AuthorId = akhmatova.Id, PublishedYear = 1935 },

                new BookEntity { Title = "Черный человек", AuthorId = yesenin.Id, PublishedYear = 1925 },
                new BookEntity { Title = "Исповедь хулигана", AuthorId = yesenin.Id, PublishedYear = 1923 }
            };
            await _context.Books.AddRangeAsync(books);
            await _context.SaveChangesAsync();
        }
    }
}