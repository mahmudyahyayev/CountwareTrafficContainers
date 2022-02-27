using NetTopologySuite.Geometries;

namespace CountwareTraffic.Services.Companies.Application
{
    public class CompanyDetailsDto : CompanyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string GsmNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FaxNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

    }
}
