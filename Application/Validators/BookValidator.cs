using Application.Dto_s;
using Application.Dto_s.Book;
using Domain.Constants;
using FluentValidation;

namespace Application.Validators;

internal class BookValidator : AbstractValidator<BookRequest>
{
    public BookValidator()
    {
        RuleFor(book => book.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(BookConstants.BookTitleMaxLength)
            .WithMessage($"Title must not exceed {BookConstants.BookTitleMaxLength} characters");
        
        RuleFor(book => book.PublishedYear)
            .NotEmpty()
            .WithMessage("Published year is required")
            .GreaterThan(BookConstants.BookMinYear - 1)
            .WithMessage($"Published year must be greater or equal to {BookConstants.BookMinYear}");
        
        RuleFor(book => book.AuthorId)
            .NotEmpty()
            .WithMessage("Author id is required")
            .GreaterThan(0)
            .WithMessage($"Author id must be greater or equal to 0");
    }
}