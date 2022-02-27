using CountwareTraffic.Services.Companies.Grpc;
using Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc;
using Sensormatic.Tool.Grpc.Client;
using Sensormatic.Tool.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class CountryService : IScopedSelfDependency
    {
        private readonly Country.CountryClient _countryClient;
        private readonly AsyncUnaryCallHandler _asyncUnaryCallHandler;
        public CountryService(Country.CountryClient countryClient, AsyncUnaryCallHandler asyncUnaryCallHandler)
        {
            _countryClient = countryClient;
            _asyncUnaryCallHandler = asyncUnaryCallHandler;
        }

        public async Task<GetCountryDetails> GetCountryByIdAsync(Guid countryId)
        {
            GetCountryRequest grpcRequest = new() { CountryId = countryId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_countryClient.GetCountryByIdAsync, grpcRequest, hasClientSideLog: false);

            return new GetCountryDetails
            {
                Id = new Guid(grpcResponse.CountryDetail.Id),
                CompanyId = new Guid(grpcResponse.CountryDetail.CompanyId),
                Name = grpcResponse.CountryDetail.Name,
                AuditCreateBy = new Guid(grpcResponse.CountryDetail.Audit.AuditCreateBy),
                AuditCreateDate = grpcResponse.CountryDetail.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(grpcResponse.CountryDetail.Audit.AuditModifiedBy),
                AuditModifiedDate = grpcResponse.CountryDetail.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
                Capital = grpcResponse.CountryDetail.Capital,
                ContinentCode = grpcResponse.CountryDetail.ContinentCode,
                CurrencyCode = grpcResponse.CountryDetail.CurrencyCode,
                Iso = grpcResponse.CountryDetail.Iso,
                Iso3 = grpcResponse.CountryDetail.Iso3,
                IsoNumeric = grpcResponse.CountryDetail.IsoNumeric
            };
        }

        public async Task<PagingResponse<GetCountryDetails>> GetCountriesAsync(Guid companyId, Paging paging)
        {
            GetCountriesRequest grpcRequest = new()
            {
                CompanyId = companyId.ToString(),
                PagingRequest = new PagingRequest
                {
                    Limit = paging.Limit,
                    Page = paging.Page
                }
            };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_countryClient.GetCountriesAsync, grpcRequest, hasClientSideLog: false);

            var countries = grpcResponse.CountryDetails.Select(country => new GetCountryDetails
            {
                Id = new Guid(country.Id),
                Name = country.Name,
                CompanyId = new Guid(country.CompanyId),
                AuditCreateBy = new Guid(country.Audit.AuditCreateBy),
                AuditCreateDate = country.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(country.Audit.AuditModifiedBy),
                AuditModifiedDate = country.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
                Capital = country.Capital,
                ContinentCode = country.ContinentCode,
                CurrencyCode = country.CurrencyCode,
                Iso = country.Iso,
                Iso3 = country.Iso3,
                IsoNumeric = country.IsoNumeric
            });

            return new PagingResponse<GetCountryDetails>(countries, grpcResponse.TotalCount, grpcResponse.Page, grpcResponse.Limit, grpcResponse.HasNextPage);
        }

        public async Task AddCountryAsync(Guid companyId, AddCountryRequest request)
        {
            CreateCountryRequest grpcRequest = new()
            {
                Capital = request.Capital,
                IsoNumeric = request.IsoNumeric,
                Iso3 = request.ContinentCode,
                ContinentCode = request.ContinentCode,
                Iso = request.Iso,
                CurrencyCode = request.CurrencyCode,
                CompanyId = companyId.ToString(),
                Name = request.Name
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_countryClient.AddCountryAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task ChangeCountryAsync(Guid countryId, ChangeCountryRequest request)
        {
            UpdateCountryRequest grpcRequest = new()
            {
                CountryId = countryId.ToString(),
                Capital = request.Capital,
                ContinentCode = request.ContinentCode,
                CurrencyCode = request.CurrencyCode,
                Iso = request.Iso,
                Iso3 = request.Iso3,
                IsoNumeric = request.IsoNumeric,
                Name = request.Name
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_countryClient.ChangeCountryAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task DeleteCountryAsync(Guid countryId)
        {
            DeleteCountryRequest grpcRequest = new() {  CountryId = countryId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_countryClient.DeleteCountryAsync, grpcRequest, hasClientSideLog: false);
        }




        public void Dispose() => GC.SuppressFinalize(this);
    }
}
