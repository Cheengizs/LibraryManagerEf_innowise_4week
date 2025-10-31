using Application.Profiles;
using Application.RepositoriesInterfaces;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Infrastructure.DatabaseContexts;
using Infrastructure.MapProfiles;
using Infrastructure.Repositories;
using Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagerEf.Configuration;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LibraryDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
        });

        services.AddAutoMapper(cfg => { },
            typeof(AuthorProfile),
            typeof(BookProfile),
            typeof(AuthorApplicationProfile),
            typeof(BookApplicationProfile));

        services.AddValidatorsFromAssemblyContaining<AuthorValidator>();
        services.AddValidatorsFromAssemblyContaining<BookValidator>();

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();

        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
        
        
        services.AddTransient<DbSeeder>();
        return services;
    }
}