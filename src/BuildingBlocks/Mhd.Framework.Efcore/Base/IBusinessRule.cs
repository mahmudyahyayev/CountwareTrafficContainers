namespace Mhd.Framework.Efcore
{
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}
