namespace CountwareTraffic.Services.Events.Api
{
    public class Paging
    {
        public Paging()
        {
            Limit = 10;
            Page = 1;
        }

        public int Limit { get; set; }
        public int Page { get; set; }
    }
}
