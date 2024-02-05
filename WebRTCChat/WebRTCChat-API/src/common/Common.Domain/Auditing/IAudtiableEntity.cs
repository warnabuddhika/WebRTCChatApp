namespace Common.Domain.Auditing
{
    public interface IAudtiableEntity
    {
        public DateTime? CreatedTime { get; }
        public DateTime? UpdatedTime { get; }

        public Guid? CreatedById { get; }
        public Guid? UpdatedById { get; }
    }
}
