using NetTopologySuite.Geometries;

namespace CountwareTraffic.Services.Companies.Core
{
    public record AreaAddress
    {
        private string _street;
        private Point _location;

        public string Street => _street;
        public Point Location => _location;


        private AreaAddress() { }

        public static AreaAddress Create(string street, Point location)
        {
            return new AreaAddress
            {
                _street = street,
                _location = location
            };
        }

        public static AreaAddress Create(string street, double latitude, double longitude)
        {
            return new AreaAddress
            {
                _street = street,
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
