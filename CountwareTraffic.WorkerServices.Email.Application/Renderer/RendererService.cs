using Stubble.Core.Builders;
using System.Globalization;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Email.Application
{
    public class RendererService : IRendererService
    {
        private readonly StubbleBuilder _builder;
        private readonly Stubble.Core.Settings.RenderSettings _renderSettings;

        public RendererService()
        {
            _builder = new StubbleBuilder();
            _renderSettings = new Stubble.Core.Settings.RenderSettings
            {
                CultureInfo = CultureInfo.GetCultureInfo("tr-TR"),
                SkipHtmlEncoding = true
            };
        }

        public async Task<string> RenderAsync(string template, object view)
        {
            return await _builder.Build().RenderAsync(template, view, _renderSettings);
        }
    }
}
