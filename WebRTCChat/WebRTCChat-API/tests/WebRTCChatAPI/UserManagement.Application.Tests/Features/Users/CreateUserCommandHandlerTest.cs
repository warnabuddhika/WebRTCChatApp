using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using UserManagement.Application.Features.Applicants.Constants;
using UserManagement.Application.Features.Users.Commands.CreateUser;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Tests.Features.Users
{
    public class CreateUserCommandHandlerTest
    {
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<UserManager<AppUser>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreateUserCommandHandlerTest()
        {
            _tokenServiceMock = new Mock<ITokenService>();
            _userManagerMock = new Mock<UserManager<AppUser>>();
        }

        private CreateUserCommandHandler Setup()
        {          
            return new CreateUserCommandHandler(_userManagerMock.Object, _tokenServiceMock.Object, _mapperMock.Object);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Handle_Should_ReturnsFailureResult_When_UserNameIsEmptyOrNull(string name)
        {
            var command = new CreateUserCommand
            {
                UserName = name,
                DisplayName = "DisplayName",
                Password = "Password"
            };

            var validator = new CreateUserCommandValidator();

            var result = validator.ValidateAsync(command);

            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == "User Name Cannot be empty");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Handle_Should_ReturnsFailureResult_When_DisplayNameIsEmptyOrNull(string name)
        {
            var command = new CreateUserCommand
            {
                UserName = "UserName",
                DisplayName = name,
                Password = "Password"
            };

            var validator = new CreateUserCommandValidator();

            var result = validator.ValidateAsync(command);

            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == "Display Name Cannot be empty");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Handle_Should_ReturnsFailureResult_When_PasswordIsEmptyOrNull(string password)
        {
            var command = new CreateUserCommand
            {
                UserName = "UserName",
                DisplayName = "DisplayName",
                Password = password
            };

            var validator = new CreateUserCommandValidator();

            var result = validator.ValidateAsync(command);

            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == "Password Cannot be empty");
        }

        [Theory]
        [InlineData("TestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserName")]
        public void Handle_Should_ReturnsFailureResult_WhenUserNameLengthExceededMaxLength(string name)
        {
            var command = new CreateUserCommand
            {
                UserName = name,
                DisplayName = "DisplayName",
                Password = "Password"
            };

            var validator = new CreateUserCommandValidator();

            var result = validator.ValidateAsync(command);
            
            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == $"User Name Cannot contain more than {UserConstants.UserNameMaxLength} characters");
        }

        [Theory]
        [InlineData("TestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserName")]
        public void Handle_Should_ReturnsFailureResult_WhenDisplayNameLengthExceededMaxLength(string name)
        {
            var command = new CreateUserCommand
            {
                UserName = "UserName",
                DisplayName = name,
                Password = "Password"
            };

            var validator = new CreateUserCommandValidator();

            var result = validator.ValidateAsync(command);

            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == $"Display Name Cannot contain more than {UserConstants.DisplayNameMaxLength} characters");
        }

        [Theory]
        [InlineData("TestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserName")]
        public void Handle_Should_ReturnsFailureResult_WhenPasswordLengthExceededMaxLength(string password)
        {
            var command = new CreateUserCommand
            {
                UserName = "UserName",
                DisplayName = "DisplayName",
                Password = password
            };

            var validator = new CreateUserCommandValidator();

            var result = validator.ValidateAsync(command);

            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == $"Password Cannot be longer than {UserConstants.PasswordMaxLength} characters");
        }

    }
}
