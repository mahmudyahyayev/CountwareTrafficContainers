using Convey;
using Convey.CQRS.Commands;

namespace CountwareTraffic.WorkerServices.Email.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
            => builder
                .AddCommandHandlers()
                .AddInMemoryCommandDispatcher();
    }
}
