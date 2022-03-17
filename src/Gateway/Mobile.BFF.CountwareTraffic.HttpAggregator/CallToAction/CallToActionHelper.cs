using System.Collections.Generic;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public static class CallToActionHelper
    {
        public static List<CallToAction> Get(CallToActionType type)
        {
            var calltoaction = new List<CallToAction>();
            switch (type)
            {
                case CallToActionType.AbortCall:
                    calltoaction.Add(new CallToAction { Name = "Close", Text = "Cancel" });
                    calltoaction.Add(new CallToAction { Name = "CallToCallCenter", Text = "Call" });
                    break;

                case CallToActionType.TryAgain:
                    calltoaction.Add(new CallToAction { Name = "Close", Text = "Try Again" });
                    break;

                case CallToActionType.OkCall:
                    calltoaction.Add(new CallToAction { Name = "Close", Text = "Ok" });
                    calltoaction.Add(new CallToAction { Name = "CallToCallCenter", Text = "Call" });
                    break;

                case CallToActionType.Ok:
                    calltoaction.Add(new CallToAction { Name = "Close", Text = "Ok" });
                    break;

                case CallToActionType.GoToDevices:
                    calltoaction.Add(new CallToAction { Name = "Close", Text = "Ok" });
                    calltoaction.Add(new CallToAction { Name = "DeleteDevice", Text = "Delete Device" });
                    break;

                default:
                    break;
            }

            return calltoaction;
        }
    }
}
