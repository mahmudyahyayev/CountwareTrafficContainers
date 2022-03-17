using Convey;
using Convey.CQRS.Commands;

namespace CountwareTraffic.Workers.Sms.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
            => builder
                .AddCommandHandlers()
                .AddInMemoryCommandDispatcher();
    }
}
