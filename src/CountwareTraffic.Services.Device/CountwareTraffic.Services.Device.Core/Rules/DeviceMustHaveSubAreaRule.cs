using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceMustHaveSubAreaRule : IBusinessRule
    {
        private readonly Guid _subAreaId;

        public DeviceMustHaveSubAreaRule(Guid subAreaId)
            => _subAreaId = subAreaId;

        public bool IsBroken() => _subAreaId == Guid.Empty;
        public string Message => "Device must have subArea";
    }
}
