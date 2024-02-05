namespace Common.Domain.Auditing
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; }
    }
}
