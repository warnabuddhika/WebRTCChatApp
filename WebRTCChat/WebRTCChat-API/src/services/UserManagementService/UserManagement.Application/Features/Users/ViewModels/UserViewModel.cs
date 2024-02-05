namespace Users.Application.Features.Users.ViewModels
{
    public class UserViewModel
    {
        public required Guid UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Status { get; set; }
        public required string ProfilePictureUrl { get; set; }
        public required string Department { get; set; }
        public required bool IsResigned { get; set; }

    }
}
