using System.Collections.Generic;

namespace CountwareTraffic.WorkerServices.Email.Data
{
    public class TemplatedEmailDTO
    {
        public string SubjectTemplate { get; set; }
        public string BodyTemplate { get; set; }
        public bool IsHtml { get; set; }
        public string From { get; set; }
        public IList<string> To { get; set; }
        public IList<string> Cc { get; set; }
        public IList<string> Bc { get; set; }
    }
}
