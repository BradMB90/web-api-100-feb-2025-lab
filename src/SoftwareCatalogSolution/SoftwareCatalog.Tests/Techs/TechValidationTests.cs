using FluentValidation.TestHelper;
using SoftwareCatalog.Api.Techs;

namespace SoftwareCatalog.Tests.Techs;

[Trait("Category", "Unit")]
public class TechValidationTests
{
    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob Johnson")]
    public void NameMustBeValid(string name)
    {
        var validator = new TechCreateModelValidator();
        var model = new TechCreateModel { Name = name, Email = "test@domain.com", Sub = "testsub" };
        var validations = validator.TestValidate(model);

        Assert.True(validations.IsValid);
    }

    [Theory]
#pragma warning disable xUnit1012 // Null should only be used for nullable parameters
    [InlineData(null)]
#pragma warning restore xUnit1012 // Null should only be used for nullable parameters
    [InlineData("")]
    public void NameIsRequired(string name)
    {
        var validator = new TechCreateModelValidator();
        var model = new TechCreateModel { Name = name, Email = "test@domain.com", Sub = "testsub" };
        var validations = validator.TestValidate(model);

        Assert.False(validations.IsValid);
    }

    [Fact]
    public void EitherEmailOrPhoneNumberIsRequired()
    {
        var validator = new TechCreateModelValidator();
        var model = new TechCreateModel { Name = "Alice", Sub = "tech123" };
        var validations = validator.TestValidate(model);

        Assert.False(validations.IsValid);
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("alice@techcompany.org")]
    public void ValidEmailsPass(string email)
    {
        var validator = new TechCreateModelValidator();
        var model = new TechCreateModel { Name = "Alice", Email = email, Sub = "tech123" };
        var validations = validator.TestValidate(model);

        Assert.True(validations.IsValid);
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("test@")]
    [InlineData("example.com")]
    public void InvalidEmailsFail(string email)
    {
        var validator = new TechCreateModelValidator();
        var model = new TechCreateModel { Name = "Alice", Email = email, Sub = "tech123" };
        var validations = validator.TestValidate(model);

        Assert.False(validations.IsValid);
    }

    [Theory]
    [InlineData("+1234567890")]
    [InlineData("9876543210")]
    public void ValidPhoneNumbersPass(string phoneNumber)
    {
        var validator = new TechCreateModelValidator();
        var model = new TechCreateModel { Name = "Alice", PhoneNumber = phoneNumber, Sub = "tech123" };
        var validations = validator.TestValidate(model);

        Assert.True(validations.IsValid);
    }

    [Theory]
    [InlineData("12345ABC")]
    [InlineData("phone-number")]
    public void InvalidPhoneNumbersFail(string phoneNumber)
    {
        var validator = new TechCreateModelValidator();
        var model = new TechCreateModel { Name = "Alice", PhoneNumber = phoneNumber, Sub = "tech123" };
        var validations = validator.TestValidate(model);

        Assert.False(validations.IsValid);
    }
}
