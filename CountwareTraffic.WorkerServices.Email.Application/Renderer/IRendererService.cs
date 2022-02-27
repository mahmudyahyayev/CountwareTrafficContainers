using Sensormatic.Tool.Ioc;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Email.Application
{
    public interface IRendererService : ISingletonDependency
    {
        Task<string> RenderAsync(string template, object view);
    }
}
