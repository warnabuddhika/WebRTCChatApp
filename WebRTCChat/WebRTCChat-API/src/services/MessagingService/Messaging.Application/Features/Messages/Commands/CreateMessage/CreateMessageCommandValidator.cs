using FluentValidation;

namespace Messaging.Application.Features.Messages.Commands.CreateMessage;

public class CreateMsssageCommandValidator : AbstractValidator<CreateMessageCommand>
{

	public CreateMsssageCommandValidator()
	{

		RuleFor(a => a.RoomId)
			.NotEmpty()
			.WithMessage("{PropertyName} Cannot be empty");


		RuleFor(a => a.SenderId)
			.NotEmpty()
			.WithMessage("{PropertyName} Cannot be empty");

	}


}
