using FluentValidation;
using UserManagement.Application.Features.Applicants.Constants;

namespace UserManagement.Application.Features.Users.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
    
        public LoginUserCommandValidator()
        {
    

            RuleFor(a => a.UserName)
                .NotEmpty()
                .WithMessage("{PropertyName} Cannot be empty")
                .MaximumLength(UserConstants.UserNameMaxLength)
                .WithMessage("{PropertyName} Cannot contain more than {MaxLength} characters");

            RuleFor(a => a.Password)
                .NotEmpty()
                .WithMessage("{PropertyName} Cannot be empty")
                .MaximumLength(UserConstants.PasswordMaxLength)
                .WithMessage("{PropertyName} Cannot contain more than {MaxLength} characters");

           
        }

   
    }
