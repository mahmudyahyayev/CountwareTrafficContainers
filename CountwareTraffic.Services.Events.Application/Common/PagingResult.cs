using System.Collections.Generic;

namespace CountwareTraffic.Services.Events.Application
{
    public interface IPagingResult
    {
        public int TotalCount { get; }
        public int Page { get; }
        public int Limit { get; }
        public bool HasNextPage { get; }
        public int Prev { get; }
        public int Next { get; }
    }

    public record PagingResult<T> : IPagingResult
    {
        public PagingResult(IEnumerable<T> data, int totalCount, int page, int limit, bool hasNextPage)
        {
            Data = data;
            TotalCount = totalCount;
            Page = page;
            Limit = limit;
            HasNextPage = hasNextPage;
            Next = HasNextPage ? page + 1 : 0;
            Prev = page > 1 ? page - 1 : 0;
        }

        public IEnumerable<T> Data { get; private set; }
        public int TotalCount { get; private set; }
        public int Page { get; private set; }
        public int Limit { get; private set; }
        public bool HasNextPage { get; private set; }
        public int Next { get; private set; }
        public int Prev { get; private set; }

        public static PagingResult<T> Empty => new PagingResult<T>(new List<T>(), 0, 1, 1, false);
    }
}
