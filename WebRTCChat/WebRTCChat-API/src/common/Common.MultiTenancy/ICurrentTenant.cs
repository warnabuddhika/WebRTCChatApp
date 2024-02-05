namespace Ascentic.Common.MultiTenancy
{
    public interface ICurrentTenant
    {
        Guid? Id { get; set; }
    }
}
