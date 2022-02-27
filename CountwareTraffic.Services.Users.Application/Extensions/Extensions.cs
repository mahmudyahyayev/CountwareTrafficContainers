﻿using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;

namespace CountwareTraffic.Services.Users.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
            => builder
                .AddCommandHandlers()
                .AddQueryHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryQueryDispatcher();
    }
}
