namespace Sensormatic.Tool.Efcore
{
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}
