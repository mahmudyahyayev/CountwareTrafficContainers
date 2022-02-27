using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensormatic.Tool.Common
{
    public interface IEmailReceiver
    {
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bc { get; set; }
    }
}
