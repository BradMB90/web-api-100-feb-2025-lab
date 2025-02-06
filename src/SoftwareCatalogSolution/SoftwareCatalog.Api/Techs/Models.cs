using FluentValidation;

namespace SoftwareCatalog.Api.Techs;

public record TechCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Sub { get; set; } = string.Empty;
}

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .When(x => string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Either Email or Phone Number is required.");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?\d+$")
            .When(x => string.IsNullOrEmpty(x.Email))
            .WithMessage("Either Email or Phone Number is required.");
    }
}

public record TechDetailsResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}

