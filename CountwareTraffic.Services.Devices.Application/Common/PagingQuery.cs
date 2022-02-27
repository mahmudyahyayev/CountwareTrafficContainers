namespace CountwareTraffic.Services.Devices.Application
{
    public record PagingQuery
    {
        public PagingQuery(int page, int limit)
        {
            Page = page;
            Limit = limit;
        }
        public int Page { get; private set; }
        public int Limit { get; private set; }
    }
}
