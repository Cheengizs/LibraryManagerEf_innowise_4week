using Application.Dto_s.Author;
using Domain.Constants;
using FluentValidation;

namespace Application.Validators;

public class AuthorValidator : AbstractValidator<AuthorRequest>
{
    public AuthorValidator()
    {
        RuleFor(author => author.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(AuthorConstants.BookTitleMaxLength).WithMessage($"Name must not exceed {AuthorConstants.BookTitleMaxLength} characters");

        RuleFor(author => author.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required");

    }
}