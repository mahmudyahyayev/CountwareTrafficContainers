using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Identity.Application
{
    public class IdentityResetPasswordRequest
    {
        public IdentityCommunicationType MessageType { get; set; }
        public IdentityUserIdentifierType IdentifierType { get; set; }
    }
}
