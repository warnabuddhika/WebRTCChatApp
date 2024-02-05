using Common.Domain.Entities;

namespace Common.Domain.Auditing;

    public class AuditableAggregateRoot<T> : AggregateRoot<T>, IAudtiableEntity where T : struct
    {
        public DateTime? CreatedTime { get; protected set; }
        public DateTime? UpdatedTime { get; protected set; }
        public Guid? CreatedById { get; protected set; }
        public Guid? UpdatedById { get; protected set; }

        protected AuditableAggregateRoot() { }

        protected AuditableAggregateRoot(T id) : base(id) { }
    }
