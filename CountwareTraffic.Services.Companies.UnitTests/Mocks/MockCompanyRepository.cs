using CountwareTraffic.Services.Companies.Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Companies.UnitTests
{
    public static class MockCompanyRepository
    {
        public static Mock<ICompanyRepository> GetCompanyRepository()
        {
            int page = 1, 
                limit = 10;

            var mockRepo = new Mock<ICompanyRepository>();

            mockRepo.Setup(r => r.GetAllAsync(page, limit))
                    .ReturnsAsync(FakeCompanyManagment.GetAll(page, limit));

            return mockRepo;
        }
    }

    public static class FakeCompanyManagment
    {
        private static IList<Company> _fakeCompanies = new List<Company>();
        private static IList<Guid> _fakeCompanyIds = new List<Guid>();
        private static CompanyAddress _fakeAddress = CompanyAddress.Create("1053 sk", "Istanbul", "Istanbul", "Turkiye", "1045", 38.8951, -77.0364);
        private static CompanyContact _fakeContract = CompanyContact.Create("553", "5555555", "my@gmail.com", "3445");

        static FakeCompanyManagment()
        {
            for (int i = 0; i < 20; i++)
            {
                var company = Company.Create($"Yahyayev Holding{i + 1}", $"Yazılım çözümleri{i + 1}", _fakeAddress, _fakeContract);
                _fakeCompanies.Add(company);
                _fakeCompanyIds.Add(company.Id);
            }
        }

        public static QueryablePagingValue<Company> GetAll(int page, int limit)
            => new QueryablePagingValue<Company>(_fakeCompanies
                                                               .Skip((page - 1) * limit)
                                                               .Take(limit)
                                                               .ToList(),
                                                 _fakeCompanies.Count);
        

        public static Company Add()
        {
            var company = Company.Create($"Yahyayev Holding", $"Yazılım çözümleri", _fakeAddress, _fakeContract);
            _fakeCompanies.Add(company);
            _fakeCompanyIds.Add(company.Id);

            return _fakeCompanies.Where(u => u.Id == company.Id).SingleOrDefault();
        }
    }
}
