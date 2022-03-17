using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Identity.Application
{
    public class IdentityUpdateUserClaimsRequest
    {
        public List<IdentityClaimRequest> Claims { get; set; }
    }
}
