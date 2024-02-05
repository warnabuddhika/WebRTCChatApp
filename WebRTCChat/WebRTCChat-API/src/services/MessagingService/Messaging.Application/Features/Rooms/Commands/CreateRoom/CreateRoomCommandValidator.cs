using FluentValidation;
using Messaging.Application.Features.Rooms.Constants;

namespace Messaging.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {

        public CreateRoomCommandValidator()
        {          

            RuleFor(a => a.RoomName)
                .NotEmpty()
                .WithMessage("{PropertyName} Cannot be empty")
                .MaximumLength(RoomConstants.NameMaxLength)
                .WithMessage("{PropertyName} Cannot contain more than {MaxLength} characters");

			RuleFor(a => a.UserId)
			   .NotEmpty()
			   .WithMessage("{PropertyName} Cannot be empty");


		}

       
    }
