using System.Collections.Generic;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public MessageResponse Message { get; set; }
    }

    public class MessageResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CallToAction> CallToActions { get; set; }
    }
    public class ResultMessage
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public CallToActionType? CallToActionType { get; set; }
    }
}
