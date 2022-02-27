namespace Sensormatic.Tool.Efcore
{
    public interface IDeletable : IInterceptor
    {
        bool AuditIsDeleted { get; set; }
    }
}
