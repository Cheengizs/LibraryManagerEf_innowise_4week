using Application.Profiles;
using Application.RepositoriesInterfaces;
using Application.Services;
using Application.Validators;
using Infrastructure.DatabaseContexts;
using Infrastructure.MapProfiles;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddTransient<DbSeeder>();


builder.Services.AddAutoMapper(cfg => { },
    typeof(AuthorProfile),
    typeof(BookProfile),
    typeof(AuthorApplicationProfile),
    typeof(BookApplicationProfile));

builder.Services.AddValidatorsFromAssemblyContaining<AuthorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BookValidator>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.SeedAsync();
}

app.Run();