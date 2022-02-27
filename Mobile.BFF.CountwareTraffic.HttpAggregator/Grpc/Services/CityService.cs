using CountwareTraffic.Services.Companies.Grpc;
using Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc;
using Sensormatic.Tool.Grpc.Client;
using Sensormatic.Tool.Ioc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class CityService : IScopedSelfDependency
    {
        private readonly City.CityClient _cityClient;
        private readonly AsyncUnaryCallHandler _asyncUnaryCallHandler;
        public CityService(City.CityClient cityClient, AsyncUnaryCallHandler asyncUnaryCallHandler)
        {
            _cityClient = cityClient;
            _asyncUnaryCallHandler = asyncUnaryCallHandler;
        }

        public async Task<GetCityDetails> GetCityByIdAsync(Guid cityId)
        {
            GetCityRequest grpcRequest = new() {  CityId = cityId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_cityClient.GetCityByIdAsync, grpcRequest, hasClientSideLog: false);

            return new GetCityDetails
            {
                Id = new Guid(grpcResponse.CityDetail.Id),
                CountryId = new Guid(grpcResponse.CityDetail.CountryId),
                Name = grpcResponse.CityDetail.Name,
                AuditCreateBy = new Guid(grpcResponse.CityDetail.Audit.AuditCreateBy),
                AuditCreateDate = grpcResponse.CityDetail.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(grpcResponse.CityDetail.Audit.AuditModifiedBy),
                AuditModifiedDate = grpcResponse.CityDetail.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime
            };
        }

        public async Task<PagingResponse<GetCityDetails>> GetCitiesAsync(Guid countryId, Paging paging)
        {
            GetCitiesRequest grpcRequest = new()
            {
                CountryId = countryId.ToString(),
                PagingRequest = new PagingRequest
                {
                    Limit = paging.Limit,
                    Page = paging.Page
                }
            };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_cityClient.GetCitiesAsync, grpcRequest, hasClientSideLog: false);

            var cities = grpcResponse.CityDetails.Select(city => new GetCityDetails
            {
                Id = new Guid(city.Id),
                Name = city.Name,
                CountryId = new Guid(city.CountryId),
                AuditCreateBy = new Guid(city.Audit.AuditCreateBy),
                AuditCreateDate = city.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(city.Audit.AuditModifiedBy),
                AuditModifiedDate = city.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
            });

            return new PagingResponse<GetCityDetails>(cities, grpcResponse.TotalCount, grpcResponse.Page, grpcResponse.Limit, grpcResponse.HasNextPage);
        }

        public async Task AddCityAsync(Guid countryId, AddCityRequest request)
        {
            CreateCityRequest grpcRequest = new()
            {
                CountryId = countryId.ToString(),
                Name = request.Name
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_cityClient.AddCityAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task ChangeCityAsync(Guid cityId, ChangeCityRequest request)
        {
            UpdateCityRequest grpcRequest = new()
            {
                CityId = cityId.ToString(),
                Name = request.Name
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_cityClient.ChangeCityAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task DeleteCityAsync(Guid cityId)
        {
            DeleteCityRequest grpcRequest = new() { CityId = cityId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_cityClient.DeleteCityAsync, grpcRequest, hasClientSideLog: false);
        }


        public void Dispose() => GC.SuppressFinalize(this);
    }
}
