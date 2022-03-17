using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Mhd.Framework.Common
{
    public static class StringExtensions
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding("Cyrillic");

        public static string GetSurname(this string @string)
        {
            char[] delimiters = { '\r', '\n', ' ' };
            string lastWord = @string.Trim()
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .LastOrDefault().Trim();
            if (@string.Trim().Equals(lastWord))
                return string.Empty;

            return lastWord;
        }

        public static string GetFirstname(this string @string)
        {
            char[] delimiters = { '\r', '\n', ' ' };
            string[] parts = @string.Trim()
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (!parts.Any())
                return string.Empty;

            if (parts.Length <= 1)
                return string.Join(" ", parts);

            string firstName = string.Empty;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                firstName = string.Format("{0} {1}", firstName, parts[i]);
            }

            return firstName.Trim();
        }

        public static bool IsNullOrWhiteSpace(this string @string) => string.IsNullOrWhiteSpace(@string);

        public static bool IsValidCitizenshipNumber(this string text)
        {
            if (text.IsNullOrWhiteSpace())
                return false;
            var expression = text.GetNumbers();
            if (expression.Length > 18)
                return false;

            if (!expression.IsNumeric())
                return false;

            if (expression.Trim().Length != 11)
                return false;

            long number = long.Parse(expression);
            if (number.ToString(CultureInfo.InvariantCulture).Length != 11)
                return false;

            long citizenshipNumber = long.Parse(expression);
            long atcno = citizenshipNumber / 100;
            long btcno = citizenshipNumber / 100;
            long c1 = atcno % 10;
            atcno = atcno / 10;
            long c2 = atcno % 10;
            atcno = atcno / 10;
            long c3 = atcno % 10;
            atcno = atcno / 10;
            long c4 = atcno % 10;
            atcno = atcno / 10;
            long c5 = atcno % 10;
            atcno = atcno / 10;
            long c6 = atcno % 10;
            atcno = atcno / 10;
            long c7 = atcno % 10;
            atcno = atcno / 10;
            long c8 = atcno % 10;
            atcno = atcno / 10;
            long c9 = atcno % 10;

            long q1 = ((10 - ((((c1 + c3 + c5 + c7 + c9) * 3) + (c2 + c4 + c6 + c8)) % 10)) % 10);
            long q2 = ((10 - (((((c2 + c4 + c6 + c8) + q1) * 3) + (c1 + c3 + c5 + c7 + c9)) % 10)) % 10);

            bool returnvalue = ((btcno * 100) + (q1 * 10) + q2 == citizenshipNumber);
            return returnvalue;
        }

        public static bool IsValidTaxNumber(this string text)
        {
            if (text.IsNullOrWhiteSpace())
                return false;
            var expression = text.GetNumbers();
            if (expression.Length > 18)
                return false;

            if (!expression.IsNumeric())
                return false;

            if (expression.Trim().Length != 10)
                return false;

            Int64 number = Int64.Parse(expression);
            if (!number.ToString("D10").Equals(expression))
                return false;


            string[] kno = expression.Select(c => c.ToString(CultureInfo.InvariantCulture)).ToArray();
            if (kno.Length == 10)
            {
                var v1 = (Convert.ToInt64(kno[0]) + 9) % 10;

                var v2 = (Convert.ToInt64(kno[1]) + 8) % 10;

                var v3 = (Convert.ToInt64(kno[2]) + 7) % 10;

                var v4 = (Convert.ToInt64(kno[3]) + 6) % 10;

                var v5 = (Convert.ToInt64(kno[4]) + 5) % 10;

                var v6 = (Convert.ToInt64(kno[5]) + 4) % 10;
                var v7 = (Convert.ToInt64(kno[6]) + 3) % 10;
                var v8 = (Convert.ToInt64(kno[7]) + 2) % 10;
                var v9 = (Convert.ToInt64(kno[8]) + 1) % 10;
                var vLastDigit = Convert.ToInt64(kno[9]);

                var v11 = (v1 * 512) % 9;
                var v22 = (v2 * 256) % 9;
                var v33 = (v3 * 128) % 9;
                var v44 = (v4 * 64) % 9;
                var v55 = (v5 * 32) % 9;
                var v66 = (v6 * 16) % 9;
                var v77 = (v7 * 8) % 9;
                var v88 = (v8 * 4) % 9;
                var v99 = (v9 * 2) % 9;

                if (v1 != 0 && v11 == 0)
                    v11 = 9;
                if (v2 != 0 && v22 == 0)
                    v22 = 9;
                if (v3 != 0 && v33 == 0)
                    v33 = 9;
                if (v4 != 0 && v44 == 0)
                    v44 = 9;
                if (v5 != 0 && v55 == 0)
                    v55 = 9;
                if (v6 != 0 && v66 == 0)
                    v66 = 9;
                if (v7 != 0 && v77 == 0)
                    v77 = 9;
                if (v8 != 0 && v88 == 0)
                    v88 = 9;
                if (v9 != 0 && v99 == 0)
                    v99 = 9;
                var toplam = v11 + v22 + v33 + v44 + v55 + v66 + v77 + v88 + v99;

                if (toplam % 10 == 0)
                    toplam = 0;
                else
                    toplam = (10 - (toplam % 10));

                if (toplam == vLastDigit)
                {
                    return true;
                }

                return false;
            }
            return false;
        }

        public static bool IsValidMailAddress(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            var isEmail = Regex.IsMatch(text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isEmail;
        }

        public static bool IsValidEmailAdress(this string text)
        {
            try
            {
                var m = new MailAddress(text);
                return !string.IsNullOrWhiteSpace(m.Address);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidPassword(this string password)
        {
            try
            {
                var hasAlphabetCharRegex = new Regex(@"[a-zA-Z]+");
                var hasNumberRegex = new Regex(@"[0-9]+");
                var hasMin8Max15CharsRegex = new Regex(@".{8,15}");
                var isValid = hasMin8Max15CharsRegex.IsMatch(password) && hasNumberRegex.IsMatch(password) && hasAlphabetCharRegex.IsMatch(password);
                return isValid;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidIban(this string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return false;
            expression = expression.Trim().ToUpper(); //IN ORDER TO COPE WITH THE REGEX BELOW
            if (Regex.IsMatch(expression, "^[A-Z0-9]"))
            {
                expression = expression.Replace(" ", string.Empty);
                string bank =
                    expression.Substring(4, expression.Length - 4) + expression.Substring(0, 4);
                const int asciiShift = 55;
                var sb = new StringBuilder();
                foreach (var c in bank)
                {
                    int v;
                    if (Char.IsLetter(c))
                        v = c - asciiShift;
                    else
                        v = int.Parse(c.ToString(CultureInfo.InvariantCulture));
                    sb.Append(v);
                }
                string checkSumString = sb.ToString();
                int checksum = int.Parse(checkSumString.Substring(0, 1));
                for (int i = 1; i < checkSumString.Length; i++)
                {
                    int v = int.Parse(checkSumString.Substring(i, 1));
                    checksum *= 10;
                    checksum += v;
                    checksum %= 97;
                }
                return checksum == 1;
            }
            return false;
        }

        public static string GetNumbers(this string text)
        {
            return new string(text.Where(char.IsDigit).ToArray());
        }

        public static string RemoveWhiteSpace(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            char[] delimiters = { '\r', '\n', ' ' };
            var parts = text.Split(delimiters,
                             StringSplitOptions.RemoveEmptyEntries);
            if (!parts.Any())
                return string.Empty;

            return string.Join(" ", parts);
        }

        public static int[] ToIntArray(this string expression, char? sep)
        {
            var sa = !sep.HasValue ? expression.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray() : expression.Split(sep.Value);
            var ia = new int[sa.Length];
            for (var i = 0; i < ia.Length; ++i)
            {
                int j;
                var s = sa[i];
                if (int.TryParse(s, out j))
                {
                    ia[i] = j;
                }
            }
            return ia;
        }

        public static bool IsValidMobilePhone(string text)
        {
            if (text.Length != 10)
                return false;

            var regexTrMobilePhone = new Regex(@"^[5][0,3,4,5,6][0-9][0-9]{7}$", RegexOptions.IgnoreCase);
            var matchTrMobilePhone = regexTrMobilePhone.Match(text);

            return matchTrMobilePhone.Success;
        }

        public static bool IsValidPhoneNumber(this string expression)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(expression))
                    return false;

                expression = expression.Trim();
                expression = expression.RemoveWhiteSpace();
                if (expression.Length > 10)
                {
                    var phonePrefixs = new List<string>
                    {
                        "0090",
                        "+90",
                        "0"
                    };
                    const StringComparison comparison = StringComparison.InvariantCultureIgnoreCase;
                    foreach (var prefix in phonePrefixs)
                    {
                        if (expression.StartsWith(prefix, comparison))
                        {
                            expression = expression.Remove(0, prefix.Length);
                        }
                    }
                }

                if (!expression.IsNumeric())
                    return false;

                if (expression.Length != 10)
                    return false;

                var isHasSequence = expression.Substring(4).ToIntArray(null).HasSequence(5);
                if (isHasSequence)
                    return false;

                var regexTrLocalPhone = new Regex(@"^[2-4][1-9][0,2,4,6,8][0-9]{7}$", RegexOptions.IgnoreCase);
                var matchTrLocalPhone = regexTrLocalPhone.Match(expression);

                var regexTrMobilePhone = new Regex(@"^[5][0,3,4,5,6][0-9][0-9]{7}$", RegexOptions.IgnoreCase);
                var matchTrMobilePhone = regexTrMobilePhone.Match(expression);

                return matchTrLocalPhone.Success || matchTrMobilePhone.Success;
            }
            catch (FormatException)
            {
                return false;
            }

        }

        public static string ClearTurkishChars(this string value)
        {
            var sb = new StringBuilder(value);
            sb = sb.Replace("ı", "i")
                   .Replace("ğ", "g")
                   .Replace("ü", "u")
                   .Replace("ş", "s")
                   .Replace("ö", "o")
                   .Replace("ç", "c")
                   .Replace("İ", "I")
                   .Replace("Ğ", "G")
                   .Replace("Ü", "U")
                   .Replace("Ş", "S")
                   .Replace("Ö", "O")
                   .Replace("Ç", "C");

            return sb.ToString();
        }

        public static string ToDashCase(this string input)
        {
            const string pattern = "[A-Z]";
            const string dash = "-";
            return Regex.Replace(input, pattern, m => (m.Index == 0 ? string.Empty : dash) + m.Value.ToLowerInvariant());
        }

        public static DateTime? ToNullableDateTime(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return null;

            if (DateTime.TryParse(value, out DateTime datetimeValue))
                return datetimeValue;

            if (DateTime.TryParseExact(value, "dd.MM.yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out datetimeValue))
                return datetimeValue;

            throw new InvalidCastException($"{value} tarih formatına çevrilemiyor.");
        }

        public static int ToInt32(this string value)
        {
            var result = 0;

            if (!string.IsNullOrWhiteSpace(value))
                int.TryParse(value, out result);

            return result;
        }

        public static Guid ToGuid(this string value)
        {
            if (Guid.TryParse(value, out Guid result))
                return result;

            return Guid.Empty;
        }

        public static Guid? ToNullableGuid(this string value)
        {
            var result = value.ToGuid();
            if (result.IsNullOrEmpty())
                return null;
            return result;
        }

        public static string ToJoinedString(this List<Guid> values, String separator)
        {
            if (!values.Any())
                return string.Empty;

            return string.Join<string>(separator, values.Select(t => string.Format("'{0}'", t.ToString())).ToList());
        }

        public static string ToShortString(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return "";

            string m = dateTime.Value.ToString("MM");
            string y = dateTime.Value.ToString("yy");

            return m + "/" + y;
        }

        public static string ToPrice(this object value, string extension = "")
        {
            if (value == null || !value.IsNumeric())
                return string.Empty;

            if (Convert.ToDecimal(value) % 1 != 0)
                return string.Format(new CultureInfo("tr-TR", false), "{0:n}", value) + (string.IsNullOrEmpty(extension) ? " TL" : " " + extension);
            else
                return string.Format(new CultureInfo("tr-TR", false), "{0:n0}", value) + (string.IsNullOrEmpty(extension) ? " TL" : " " + extension);
        }

        public static string ToPhoneNumber(this string value)
        {
            return $"+90{value.FixDeviceNumber()}";

        }

        private static readonly Dictionary<string, string> ForeignCharacters = new Dictionary<string, string>
            {
                { "ÀÁÂÃÄÅǺĀĂĄǍΑΆẢẠẦẪẨẬẰẮẴẲẶА", "A" },
                { "àáâãåǻāăąǎªαάảạầấẫẩậằắẵẳặа", "a" },
                { "ÈÉÊËĒĔĖĘĚΕΈẼẺẸỀẾỄỂỆЕЭ", "E" },
                { "èéêëēĕėęěέεẽẻẹềếễểệеэ", "e" },
                { "ÌÍÎÏĨĪĬǏĮΗΉΊΙΪỈỊИЫ", "I" },
                { "ìíîïĩīĭǐįηήίιϊỉịиыї", "i" },
            };

    }
}
