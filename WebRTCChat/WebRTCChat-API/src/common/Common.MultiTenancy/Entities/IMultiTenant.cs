namespace Ascentic.Common.MultiTenancy.Entities
{
    public interface IMultiTenant
    {
        Guid? TenantId { get; }
    }
}
