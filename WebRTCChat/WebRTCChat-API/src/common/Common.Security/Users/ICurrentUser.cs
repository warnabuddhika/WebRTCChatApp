namespace Common.Security.Users
{
    public interface ICurrentUser
    {
        public Guid? Id { get; set; }
    }
}
