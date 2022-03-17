namespace Mhd.Framework.Efcore
{
    public interface IDeletable : IInterceptor
    {
        bool AuditIsDeleted { get; set; }
    }
}
