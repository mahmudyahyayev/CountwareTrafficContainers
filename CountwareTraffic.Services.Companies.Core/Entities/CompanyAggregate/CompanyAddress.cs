using NetTopologySuite.Geometries;

namespace CountwareTraffic.Services.Companies.Core
{
    public record CompanyAddress
    {
        private string _street;
        private string _city;
        private string _state;
        private string _country;
        private string _zipCode;
        private Point _location;

        public string Street => _street;
        public string City => _city;
        public string State => _state;
        public string Country => _country;
        public string ZipCode => _zipCode;
        public Point Location => _location;


        private CompanyAddress() { }

        public static CompanyAddress Create(string street, string city, string state, string country, string zipCode, Point location)
        {
            return new CompanyAddress
            {
                _street = street,
                _city = city,
                _state = state,
                _country = country,
                _zipCode = zipCode,
                _location = location
            };
        }

        public static CompanyAddress Create(string street, string city, string state, string country, string zipCode, double latitude, double longitude)
        {
            return new CompanyAddress
            {
                _street = street,
                _city = city,
                _state = state,
                _country = country,
                _zipCode = zipCode,
                _location = CreateLoaction(latitude, longitude)
            };
        }

        private static Point CreateLoaction(double latitude, double longitude)
        {
            Coordinate coordinate = new() { X = longitude, Y = latitude };

            Point point = new(coordinate);

            point.SRID = 4326;

            return point;
        }
    }
}
