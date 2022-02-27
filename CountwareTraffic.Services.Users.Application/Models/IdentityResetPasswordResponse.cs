using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Users.Application
{
    public class IdentityResetPasswordResponse
    {
        public int ExpiresIn { get; set; }
        public string PhoneNumber { get; set; }
    }
}
