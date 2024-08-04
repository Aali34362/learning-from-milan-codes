using FluentAssertions;
using FluentValidation.TestHelper;
using Modules.Users.Application.UnitTests.Data.Users;
using Modules.Users.Application.UnitTests.Utilities;
using Modules.Users.Application.Users.RegisterUser;
using Modules.Users.Application.ValidationErrors;
using Xunit;

namespace Modules.Users.Application.UnitTests.Users.Commands.RegisterUser;

public class RegisterUserCommandValidatorTests
{
    [Theory]
    [ClassData(typeof(RegisterUserCommandData))]
    public void Validate_ShouldBeValidAndHaveNoErrors_WhenCommandIsValid(RegisterUserCommand command)
    {
        // Arrange
        var validator = new RegisterUserCommandValidator();

        // Act
        TestValidationResult<RegisterUserCommand> result = validator.TestValidate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [ClassData(typeof(RegisterUserCommandData))]
    public void Validate_ShouldHaveErrors_WhenIdentityProviderIdIsEmpty(RegisterUserCommand command)
    {
        // Arrange
        command = command with
        {
            IdentityProviderId = string.Empty
        };

        var validator = new RegisterUserCommandValidator();

        // Act
        TestValidationResult<RegisterUserCommand> result = validator.TestValidate(command);

        // Assert
        result.GetErrors().Should().Contain(UserValidationErrors.IdentityIsRequired);
    }

    [Theory]
    [ClassData(typeof(RegisterUserCommandData))]
    public void Validate_ShouldHaveErrors_WhenEmailIsEmpty(RegisterUserCommand command)
    {
        // Arrange
        command = command with
        {
            Email = string.Empty
        };

        var validator = new RegisterUserCommandValidator();

        // Act
        TestValidationResult<RegisterUserCommand> result = validator.TestValidate(command);

        // Assert
        result.GetErrors().Should().Contain(UserValidationErrors.EmailIsRequired);
    }

    [Theory]
    [ClassData(typeof(RegisterUserCommandData))]
    public void Validate_ShouldHaveErrors_WhenFirstNameIsEmpty(RegisterUserCommand command)
    {
        // Arrange
        command = command with
        {
            FirstName = string.Empty
        };

        var validator = new RegisterUserCommandValidator();

        // Act
        TestValidationResult<RegisterUserCommand> result = validator.TestValidate(command);

        // Assert
        result.GetErrors().Should().Contain(UserValidationErrors.FirstNameIsRequired);
    }

    [Theory]
    [ClassData(typeof(RegisterUserCommandData))]
    public void Validate_ShouldHaveErrors_WhenLastNameIsEmpty(RegisterUserCommand command)
    {
        // Arrange
        command = command with
        {
            LastName = string.Empty
        };

        var validator = new RegisterUserCommandValidator();

        // Act
        TestValidationResult<RegisterUserCommand> result = validator.TestValidate(command);

        // Assert
        result.GetErrors().Should().Contain(UserValidationErrors.LastNameIsRequired);
    }
}
