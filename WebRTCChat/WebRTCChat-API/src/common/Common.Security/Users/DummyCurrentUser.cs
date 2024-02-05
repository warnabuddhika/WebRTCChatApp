namespace Common.Security.Users
{
    public class DummyCurrentUser : ICurrentUser
    {
        public Guid? Id { get; set; }
    }
}
