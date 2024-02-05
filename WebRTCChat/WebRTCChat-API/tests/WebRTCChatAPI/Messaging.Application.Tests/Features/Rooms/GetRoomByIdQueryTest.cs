using AutoMapper;
using Common.Domain.Repositories;
using Messaging.Application.Features.Rooms.Queries.GetAllRooms;
using Messaging.Application.Features.Rooms.Queries.GetRoomById;
using Messaging.Domain.Dtos;
using Messaging.Domain.Entities;
using Messaging.Domain.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace Messaging.Application.Tests.Features.Rooms
{
    public class GetRoomByIdQueryTest
    {
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetRoomByIdQueryTest()
        {
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
        }

        private GetRoomByIdQueryHandler Setup()
        {
            _roomRepositoryMock.Setup(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
            return new GetRoomByIdQueryHandler(_roomRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_Should_GetRooms_WhenExist()
        {
            var cancellationToken = new CancellationToken();

            var room = Room.Create("name");
      
            _roomRepositoryMock
                .Setup(r => r.GetAsync(
                    It.IsAny<Expression<Func<Room, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(room);

            _mapperMock
                .Setup(m => m.Map<RoomDto>(room))
                .Returns( new RoomDto
                {
                    DisplayName = "name",
                    RoomId  = Guid.NewGuid(),
                    RoomName = "name",
                    CountMember = 1,
                    UserId = "UserId",
                    UserName = "UserName"
                });

            var handler = Setup();

            var result = await handler.Handle(new GetRoomByIdQuery(), cancellationToken);

            Assert.NotNull(result);
            

            _roomRepositoryMock.Verify(
                r => r.GetAsync(
                    It.IsAny<Expression<Func<Room, bool>>>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            _mapperMock.Verify(m => m.Map<RoomDto>(room), Times.Once);
        }
    }
}
