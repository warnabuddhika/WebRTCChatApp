using FluentValidation;
using UserManagement.Application.Features.Applicants.Constants;

namespace UserManagement.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {

        public CreateUserCommandValidator()
        {          

            RuleFor(a => a.UserName)
                .NotEmpty()
                .WithMessage("{PropertyName} Cannot be empty")
                .MaximumLength(UserConstants.UserNameMaxLength)
                .WithMessage("{PropertyName} Cannot contain more than {MaxLength} characters");

            RuleFor(a => a.DisplayName)
                .NotEmpty()
                .WithMessage("{PropertyName} Cannot be empty")
                .MaximumLength(UserConstants.DisplayNameMaxLength)
                .WithMessage("{PropertyName} Cannot contain more than {MaxLength} characters");

			RuleFor(a => a.Password)
				.NotEmpty()
				.WithMessage("{PropertyName} Cannot be empty")
				.MaximumLength(UserConstants.PasswordMaxLength)
				.WithMessage("{PropertyName} Cannot be longer than {MaxLength} characters");
                
        }

       
    }
