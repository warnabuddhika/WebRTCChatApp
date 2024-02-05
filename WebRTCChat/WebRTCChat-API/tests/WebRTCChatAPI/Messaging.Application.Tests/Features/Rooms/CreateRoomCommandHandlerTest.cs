using AutoMapper;
using Common.Domain.Repositories;
using Messaging.Application.Features.Rooms.Commands.CreateRoom;
using Messaging.Domain.Entities;
using Messaging.Domain.Interfaces;
using Moq;

namespace Messaging.Application.Tests.Features.Rooms
{
    public class CreateRoomCommandHandlerTest
    {
      
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateRoomCommandHandlerTest()
        {
            _mapperMock = new Mock<IMapper>();
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        private CreateRoomCommandHandler Setup()
        {
            _roomRepositoryMock.Setup(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            return new CreateRoomCommandHandler(_mapperMock.Object, _roomRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Handle_Should_ReturnsFailureResult_When_RoomNameIsEmptyOrNull(string name)
        {
            var command = new CreateRoomCommand
            {
                RoomName = name,
                UserId = Guid.NewGuid(),
            };

            var validator = new CreateRoomCommandValidator();

            var result = validator.ValidateAsync(command);

            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == "Room Name Cannot be empty");
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Handle_Should_ReturnsFailureResult_When_UserIdIsEmptyOrNull(string userId)
        {
            var command = new CreateRoomCommand
            {
                RoomName = "RoomName",
                UserId = Guid.Empty
            };

            var validator = new CreateRoomCommandValidator();

            var result = validator.ValidateAsync(command);

            Assert.False(result.Result.IsValid);
            Assert.Contains(result.Result.Errors, e => e.ErrorMessage == "User Id Cannot be empty");
        }

        [Fact]
        public async Task Handle_Should_CallInsertOnRepository_WhenDataIsValid()
        {
            var cancelationTocken = new CancellationToken();
        

            var command = new CreateRoomCommand
            {
                RoomName = "RoomName",
                UserId = Guid.NewGuid()
            };

            var validator = new CreateRoomCommandValidator();
            var validationResult = validator.ValidateAsync(command);

            var handler = Setup();

            await handler.Handle(command, cancelationTocken);

            _roomRepositoryMock.Verify(
                x => x.InsertAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()),
                 Times.Once);
        }

    }
}
