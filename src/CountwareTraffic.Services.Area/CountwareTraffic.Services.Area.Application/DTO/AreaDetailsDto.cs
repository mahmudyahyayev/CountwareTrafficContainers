using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class AreaDetailsDto : AreaDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AreaTypeName { get; set; }
        public Guid DistrictId { get; set; }
        public string GsmNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string EmailAddress { get; set; }
        public string FaxNumber { get; set; }
        public string Street { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int AreaTypeId { get; set; }
    }
}
