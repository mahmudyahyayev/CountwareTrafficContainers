using Mhd.Framework.Ioc;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Email.Application
{
    public interface IRendererService : ISingletonDependency
    {
        Task<string> RenderAsync(string template, object view);
    }
}
