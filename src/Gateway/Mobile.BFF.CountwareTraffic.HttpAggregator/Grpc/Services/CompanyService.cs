using CountwareTraffic.Services.Areas.Grpc;
using Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc;
using Mhd.Framework.Grpc.Client;
using Mhd.Framework.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class CompanyService : IScopedSelfDependency
    {
        private readonly Company.CompanyClient _companyClient;
        private readonly AsyncUnaryCallHandler _asyncUnaryCallHandler;
        public CompanyService(Company.CompanyClient companyClient, AsyncUnaryCallHandler asyncUnaryCallHandler)
        {
            _companyClient = companyClient;
            _asyncUnaryCallHandler = asyncUnaryCallHandler;
        }

        public async Task<GetCompanyDetails> GetCompanyByIdAsync(Guid companyId)
        {
            GetCompanyRequest grpcRequest = new() { CompanyId = companyId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_companyClient.GetCompanyByIdAsync, grpcRequest, hasClientSideLog: false);

            return new GetCompanyDetails
            {
                Id = new Guid(grpcResponse.CompanyDetail.Id),
                Name = grpcResponse.CompanyDetail.Name,
                Description = grpcResponse.CompanyDetail.Description,
                City = grpcResponse.CompanyDetail.City,
                Country = grpcResponse.CompanyDetail.Country,
                State = grpcResponse.CompanyDetail.State,
                AuditCreateBy = new Guid(grpcResponse.CompanyDetail.Audit.AuditCreateBy),
                AuditCreateDate = grpcResponse.CompanyDetail.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(grpcResponse.CompanyDetail.Audit.AuditModifiedBy),
                AuditModifiedDate = grpcResponse.CompanyDetail.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
                EmailAddress = grpcResponse.CompanyDetail.EmailAddress,
                FaxNumber = grpcResponse.CompanyDetail.FaxNumber,
                GsmNumber = grpcResponse.CompanyDetail.GsmNumber,
                Latitude = grpcResponse.CompanyDetail.Latitude,
                Longitude = grpcResponse.CompanyDetail.Longitude,
                PhoneNumber = grpcResponse.CompanyDetail.PhoneNumber,
                Street = grpcResponse.CompanyDetail.Street,
                ZipCode = grpcResponse.CompanyDetail.ZipCode
            };
        }

        public async Task<PagingResponse<GetCompanyDetails>> GetCompaniesAsync(Paging paging)
        {
            GetCompaniesRequest grpcRequest = new()
            {
                PagingRequest = new PagingRequest
                {
                    Limit = paging.Limit,
                    Page = paging.Page
                }
            };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_companyClient.GetCompaniesAsync, grpcRequest, hasClientSideLog: false);

            var companies = grpcResponse.CompanyDetails.Select(company => new GetCompanyDetails
            {
                Id = new Guid(company.Id),
                Name = company.Name,
                Description = company.Description,
                AuditCreateBy = new Guid(company.Audit.AuditCreateBy),
                AuditCreateDate = company.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(company.Audit.AuditModifiedBy),
                AuditModifiedDate = company.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
                EmailAddress = company.EmailAddress,
                FaxNumber = company.FaxNumber,
                GsmNumber = company.GsmNumber,
                Latitude = company.Latitude,
                Longitude = company.Longitude,
                PhoneNumber = company.PhoneNumber,
                Street = company.Street,
                City = company.City,
                ZipCode = company.ZipCode,
                State = company.State,
                Country = company.Country
            });

            return new PagingResponse<GetCompanyDetails>(companies, grpcResponse.TotalCount, grpcResponse.Page, grpcResponse.Limit, grpcResponse.HasNextPage);
        }

        public async Task AddCompanyAsync(AddCompanyRequest request)
        {
            CreateCompanyRequest grpcRequest = new()
            {
                City = request.City,
                Country = request.Country,
                EmailAddress = request.EmailAddress,
                Description = request.Description,
                Name = request.Name,
                FaxNumber = request.FaxNumber,
                GsmNumber = request.GsmNumber,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                PhoneNumber = request.PhoneNumber,
                State = request.State,
                Street = request.Street,
                ZipCode = request.ZipCode
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_companyClient.AddCompanyAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task ChangeCompanyAsync(Guid companyId, ChangeCompanyRequest request)
        {
            UpdateCompanyRequest grpcRequest = new()
            {
                City = request.City,
                CompanyId = companyId.ToString(),
                Country = request.Country,
                State = request.State,
                ZipCode = request.ZipCode,
                Description = request.Description,
                Name = request.Name,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                GsmNumber = request.GsmNumber,
                Street = request.Street
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_companyClient.ChangeCompanyAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task DeleteCompanyAsync(Guid companyId)
        {
            DeleteCompanyRequest grpcRequest = new() { CompanyId = companyId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_companyClient.DeleteCompanyAsync, grpcRequest, hasClientSideLog: false);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
