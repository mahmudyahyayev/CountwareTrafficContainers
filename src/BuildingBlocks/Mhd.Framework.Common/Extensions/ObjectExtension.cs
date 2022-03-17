using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Mhd.Framework.Common
{
    public static class ObjectExtension
    {
        public static bool IsNumeric(this object @object, NumberStyles numberStyle = NumberStyles.Any)
        {
            if (@object == null)
                return false;

            bool isNumumeric = Double.TryParse(@object.ToString(),
                numberStyle,
                NumberFormatInfo.CurrentInfo,
                out double retNum);

            return isNumumeric;
        }
        public static bool IsList(this object @object)
        {
            if (@object == null) return false;

            return @object is IList &&
                   @object.GetType().IsGenericType &&
                   @object.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }
    }
}
