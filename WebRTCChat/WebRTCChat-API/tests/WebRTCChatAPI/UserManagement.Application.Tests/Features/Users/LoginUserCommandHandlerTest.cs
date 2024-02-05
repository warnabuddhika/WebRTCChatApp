using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using UserManagement.Application.Features.Applicants.Constants;
using UserManagement.Application.Features.Users.Commands.CreateUser;
using UserManagement.Application.Features.Users.Commands.LoginUser;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Tests.Features.Users
{
    public class LoginUserCommandHandlerTest
    {
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<UserManager<AppUser>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<SignInManager<AppUser>> _signInManagerMock;

        public LoginUserCommandHandlerTest()
        {
            _tokenServiceMock = new Mock<ITokenService>();
            _userManagerMock = new Mock<UserManager<AppUser>>();
        }

        private LoginUserCommandHandler Setup()
        {          
            return new LoginUserCommandHandler(_userManagerMock.Object, _signInManagerMock.Object, _tokenServiceMock.Object, _mapperMock.Object);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Handle_Should_ReturnsFailureResult_When_UserNameIsEmptyOrNull(string name)
        {
            var command = new LoginUserCommand
            {
                UserName = name,               
                Password = "Password"
            };

            var validator = new LoginUserCommandValidator();

            var result = validator.ValidateAsync(command);

            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == "User Name Cannot be empty");
        }
       

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Handle_Should_ReturnsFailureResult_When_PasswordIsEmptyOrNull(string password)
        {
            var command = new LoginUserCommand
            {
                UserName = "UserName",                
                Password = password
            };

            var validator = new LoginUserCommandValidator();

            var result = validator.ValidateAsync(command);

            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == "Password Cannot be empty");
        }

        [Theory]
        [InlineData("TestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserName")]
        public void Handle_Should_ReturnsFailureResult_WhenUserNameLengthExceededMaxLength(string name)
        {
            var command = new LoginUserCommand
            {
                UserName = name,                
                Password = "Password"
            };

            var validator = new LoginUserCommandValidator();

            var result = validator.ValidateAsync(command);
            
            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == $"User Name Cannot contain more than {UserConstants.UserNameMaxLength} characters");
        }
      

        [Theory]
        [InlineData("TestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserNameTestUserName")]
        public void Handle_Should_ReturnsFailureResult_WhenPasswordLengthExceededMaxLength(string password)
        {
            var command = new LoginUserCommand
            {
                UserName = "UserName",                
                Password = password
            };

            var validator = new LoginUserCommandValidator();

            var result = validator.ValidateAsync(command);

            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == $"Password Cannot contain more than {UserConstants.PasswordMaxLength} characters");
        }

    }
}
