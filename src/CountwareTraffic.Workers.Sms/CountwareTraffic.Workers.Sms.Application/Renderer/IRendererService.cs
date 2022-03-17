using Mhd.Framework.Ioc;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Sms.Application
{
    public interface IRendererService : ISingletonDependency
    {
        Task<string> RenderAsync(string template, object view);
    }
}
