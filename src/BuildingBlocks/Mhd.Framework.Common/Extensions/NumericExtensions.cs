using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhd.Framework.Common
{
    public static class NumericExtensions
    {
        public static int RoundOff(this int i)
        {
            return ((int)Math.Round(i / 10.0)) * 10;
        }
        public static decimal CeilingDecimals(this decimal d, int decimals)
        {
            var precision = (decimal)Math.Pow(10d, decimals);

            return Math.Ceiling(d * precision) / precision;

        }
        public static double CeilingDecimals(this double d, int decimals)
        {
            var precision = Math.Pow(10d, decimals);

            return Math.Ceiling(d * precision) / precision;

        }
        public static decimal FloorDecimals(this decimal d, int decimals)
        {
            var precision = (decimal)Math.Pow(10d, decimals);

            return Math.Floor(d * precision) / precision;

        }
        public static double FloorDecimals(this double d, int decimals)
        {
            var precision = Math.Pow(10d, decimals);

            return Math.Floor(d * precision) / precision;

        }
        public static double Truncate(this double value, int digits)
        {
            var mult = Math.Pow(10.0, digits);
            var result = Math.Truncate(mult * value) / mult;
            return result;
        }
        public static int RoundUpKMB(this double num)
        {
            var i = Math.Pow(10, (int)Math.Max(0, Math.Log10(num) - 2));
            num = num / i * i;

            if (num >= 1000000000)
                return (num / 1000000000D).RoundUp();
            if (num >= 1000000)
                return (num / 1000000D).RoundUp();
            if (num >= 1000)
                return (num / 1000D).RoundUp();

            return num.RoundUp();
        }
        public static int RoundOff(this double i)=> ((int)Math.Round(i / 10.0)) * 10;
        public static int RoundOff(this decimal i) => ((int)Math.Round(i / 10.0m)) * 10;
        public static int RoundUp(this double i)=> (int)(i - Math.Ceiling(i) > 0 ? Math.Ceiling(i) + 1 : Math.Ceiling(i));
        public static int RoundUp(this decimal i) => (int)(i - Math.Ceiling(i) > 0 ? Math.Ceiling(i) + 1 : Math.Ceiling(i));
        public static bool AlmostEquals(this decimal first, decimal second, decimal precision)=> (Math.Abs(first - second) <= precision);
        public static bool EqualsZero(this decimal @decimal)
        {
            const decimal decimal2 = (decimal)0.0;
            const decimal precision = (decimal)0.0000001;
            return (Math.Abs(@decimal - decimal2) <= precision);
        }
        public static bool HasSequence(this int[] expression, int maxSequence)
        {
            int counterHit = 0;
            int maxHit = maxSequence - 1;

            if (expression.Distinct().Count() == 1)
                return true;

            for (int idx = 0; idx < expression.Count() - 1; idx++)
            {
                if (expression[idx] == expression[idx + 1] - 1)
                {
                    counterHit++;
                    if (counterHit == maxHit)
                        return true;
                }
                else
                    counterHit = 0;
            }

            
            counterHit = 0;
            for (int idx = 0; idx < expression.Count() - 1; idx++)
            {
                if (expression[idx] == expression[idx + 1] + 1)
                {
                    counterHit++;
                    if (counterHit == maxHit)
                        return true;
                }
                else
                    counterHit = 0;
            }

            for (int idx = 0; idx < expression.Count() - 1; idx++)
            {
                if (expression[idx] == expression[idx + 1])
                {
                    counterHit++;
                    if (counterHit == maxHit)
                        return true;
                }
                else
                    counterHit = 0;
            }


            return false;
        }
        public static bool IsNullOrNegative(this decimal? target) => (target == null || target < 0);
        public static bool IsNullOrLessThanOrEqualToZero(this decimal? target) => (target == null || target <= 0);
        public static bool IsLessThanOrEqualToZero(this decimal target) => (target <= 0);
        public static bool IsNullOrNegative(this double? target) => (target == null || target < 0);
        public static string FixDeviceNumber(this string deviceNumber)
        {
            if (deviceNumber.IsNullOrWhiteSpace())
                return string.Empty;

            var validations = new List<string> { "0090", "90", "+90", "0" };
            var result = deviceNumber.Trim().Replace(" ", "").GetNumbers();
            foreach (var validation in validations)
            {
                if (!result.StartsWith(validation)) continue;

                result = result.Substring(validation.Length, result.Length - validation.Length);
                break;
            }
            return result;
        }
    }
}
