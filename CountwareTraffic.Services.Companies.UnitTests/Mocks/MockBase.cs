using CountwareTraffic.Services.Companies.Application;
using CountwareTraffic.Services.Companies.Core;
using CountwareTraffic.Services.Companies.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Sensormatic.Tool.Queue;
using System;

namespace CountwareTraffic.Services.Companies.UnitTests
{
    public static class MockBase
    {
        public static Mock<IUnitOfWork> MockUnitOfWork
            => new Mock<IUnitOfWork>(MockDbContext.Object, null);  //----> service provider gondermek lazim. ve registr islemleri yapilmasi gerek. Suanda eksik ve hatali.

        public static Mock<AreaDbContext> MockDbContext
            => new Mock<AreaDbContext>(new DbContextOptionsBuilder().Options, MockDbProvider.Object);

        public static Mock<AreaDbProvider> MockDbProvider
            => new Mock<AreaDbProvider>(MockQueueEventMapper.Object, MockIdentityService.Object, MockQueueService.Object);

        public static Mock<IQueueEventMapper> MockQueueEventMapper
              => new Mock<IQueueEventMapper>();

        public static Mock<IQueueService> MockQueueService
           => new Mock<IQueueService>();

        public static Mock<IIdentityService> MockIdentityService
                      => new Mock<IIdentityService>();

        //Bu kisimda kaldim kendime not Mahmud Yahyayev
        public static Mock<IServiceProvider> MockServiceProvider()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            //repository registr etmek gerek. Cunki unit ow work repoyus ServiceProvider uzerinden get etmektedir.
            //serviceProvider.Setup(x=>x.)

            return serviceProvider;
        }
    }
}
