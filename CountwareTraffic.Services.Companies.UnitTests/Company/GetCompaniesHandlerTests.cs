using CountwareTraffic.Services.Companies.Application;
using CountwareTraffic.Services.Companies.Core;
using CountwareTraffic.Services.Companies.Infrastructure;
using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CountwareTraffic.Services.Companies.UnitTests
{
    public class GetCompaniesHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public GetCompaniesHandlerTests()
            => _unitOfWork = MockBase.MockUnitOfWork;


        [Fact]
        public async Task GetCompanies()
        {
            var handler = new GetCompaniesHandler(_unitOfWork.Object);

            var result = await handler.HandleAsync(new GetCompanies
            {
                PagingQuery = new PagingQuery(1, 10)
            });

            result.ShouldBeOfType<PagingResult<CompanyDetailsDto>>();
        }
    }
}
