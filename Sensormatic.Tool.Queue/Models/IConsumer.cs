using System.Threading.Tasks;

namespace Sensormatic.Tool.Queue
{
    public interface IConsumer<T> : IConsumer where T : class
    {
        Task ConsumeAsync(T message);
    }

    public interface IConsumer
    {
    }
}
