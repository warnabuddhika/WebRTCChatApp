namespace Ascentic.Common.MultiTenancy
{
    public class DummyCurrentTenant : ICurrentTenant
    {
        public Guid? Id { get; set; }
    }
}
