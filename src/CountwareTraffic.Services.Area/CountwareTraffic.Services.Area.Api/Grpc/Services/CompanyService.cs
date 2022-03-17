using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Countware.Traffic.Observability;
using CountwareTraffic.Services.Areas.Application;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace CountwareTraffic.Services.Areas.Grpc
{
    [Authorize]
    public class CompanyService : Company.CompanyBase
    {
        private readonly ILogger<CompanyService> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public CompanyService(ILogger<CompanyService> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public override async Task<GetCompanyDetailResponse> GetCompanyById(GetCompanyRequest request, ServerCallContext context)
        {
            var company = await _queryDispatcher.QueryAsync(new GetCompany { CompanyId = request._CompanyId });

            GetCompanyDetailResponse response = new ();

            response.CompanyDetail = new CompanyDetail
            {
                Id = company.Id.ToString(),
                City = company.City,
                Country = company.Country,
                Description = company.Description,
                EmailAddress = company.EmailAddress,
                FaxNumber = company.FaxNumber,
                GsmNumber = company.GsmNumber,
                Latitude = company.Latitude,
                Longitude = company.Longitude,
                Name = company.Name,
                PhoneNumber = company.PhoneNumber,
                State = company.State,
                Street = company.Street,
                ZipCode = company.ZipCode,
                Audit = new Audit
                {
                    AuditCreateBy = company.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(company.AuditCreateDate),
                    AuditModifiedBy = company.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(company.AuditModifiedDate),
                }
            };

            return response;
        }

        public override async Task<CompanyPagingResponse> GetCompanies(GetCompaniesRequest request, ServerCallContext context)
        {
            var correlationId = context.GetCorrelationId();

            var pagingCompanies = await _queryDispatcher.QueryAsync(new GetCompanies
            {
                PagingQuery = new PagingQuery(request.PagingRequest.Page, request.PagingRequest.Limit)
            });

            CompanyPagingResponse response = new()
            {
                TotalCount = pagingCompanies.TotalCount,
                HasNextPage = pagingCompanies.HasNextPage,
                Page = pagingCompanies.Page,
                Limit = pagingCompanies.Limit,
                Next = pagingCompanies.Next,
                Prev = pagingCompanies.Prev
            };

            pagingCompanies.Data.ToList().ForEach(company => response.CompanyDetails.Add(new CompanyDetail
            {
                Id = company.Id.ToString(),
                City = company.City,
                Country = company.Country,
                Description = company.Description,
                EmailAddress = company.EmailAddress,
                FaxNumber = company.FaxNumber,
                GsmNumber = company.GsmNumber,
                Latitude = company.Latitude,
                Longitude = company.Longitude,
                Name = company.Name,
                PhoneNumber = company.PhoneNumber,
                State = company.State,
                Street = company.Street,
                ZipCode = company.ZipCode,
                Audit = new Audit
                {
                    AuditCreateBy = company.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(company.AuditCreateDate),
                    AuditModifiedBy = company.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(company.AuditModifiedDate),
                }
            }));

            return response;
        }


        public override async Task<CreateSuccessResponse> AddCompany(CreateCompanyRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new CreateCompany
            {
                Name = request.Name,
                City = request.City,
                EmailAddress = request.EmailAddress,
                Country = request.Country,
                Description = request.Description,
                FaxNumber = request.FaxNumber,
                GsmNumber = request.GsmNumber,
                Latitude = request.Latitude.HasValue ? request.Latitude.Value : 0,
                Longitude = request.Longitude.HasValue ? request.Longitude.Value : 0,
                PhoneNumber = request.PhoneNumber,
                State = request.State,
                Street = request.Street,
                ZipCode = request.ZipCode
            });

            return new CreateSuccessResponse { Created = "Created" };
        }

        public override async Task<UpdateSuccessResponse> ChangeCompany(UpdateCompanyRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new UpdateCompany
            {
                ComapnyId = request._CompanyId,
                City = request.City,
                Country = request.Country,
                Description = request.Description,
                EmailAddress = request.EmailAddress,
                FaxNumber = request.FaxNumber,
                GsmNumber = request.GsmNumber,
                Latitude = request.Latitude.HasValue ? request.Latitude.Value : 0,
                Longitude = request.Longitude.HasValue ? request.Longitude.Value : 0,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                State = request.State,
                Street = request.Street,
                ZipCode = request.ZipCode
            });

            return new UpdateSuccessResponse { Updated = "Updated" };
        }

        public override async Task<DeleteSuccessResponse> DeleteCompany(DeleteCompanyRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new DeleteCompany { CompanyId = request._CompanyId });
            return new DeleteSuccessResponse { Deleted = "Deleted" };
        }
    }
}
