using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Sys.Common.Helper
{
    public static class Extension
    {
        #region Char and string

        public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input.First().ToString().ToUpper() + input.Substring(1)
        };

        public static bool IsNotEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string EncodeString(this string str)
        {
            return str.IsNotEmpty() ? HttpUtility.HtmlEncode(str) : string.Empty;
        }

        public static string DecodeString(this string str)
        {
            return str.IsNotEmpty() ? HttpUtility.HtmlDecode(str) : string.Empty;
        }

        public static bool InsensitiveEqual(this string str, string compareStr)
        {
            return (str.IsNullOrWriteSpace() && str == compareStr) || (str.Equals(compareStr, StringComparison.InvariantCultureIgnoreCase));
        }

        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            //Thời gian bắt buộc đổi sang đầu số điện thoại mới 15/11/2018
            DateTime timeChangePhoneNumber = new DateTime(2018, 11, 15, 0, 0, 0);
            if (DateTime.Now < timeChangePhoneNumber)
            {
                var oldNumber = Regex.Match(phoneNumber, @"(09|01[2|6|8|9])+([0-9]{8})\b", RegexOptions.None).Success;
                var newNumber = Regex.Match(phoneNumber, @"(07[0|6|7|8|9]|08[1|2|3|4|5]|03[2|3|4|5|6|7|8|9]|05[6|8|9])+([0-9]{7})\b", RegexOptions.None).Success;
                return (oldNumber || newNumber);
            }
            else
            {
                var newNumber = Regex.Match(phoneNumber, @"(07[0|6|7|8|9]|08[1|2|3|4|5]|03[2|3|4|5|6|7|8|9]|05[6|8|9])+([0-9]{7})\b", RegexOptions.None).Success;
                return newNumber;
            }
        }

        public static bool IsValidEmail(this string email)
        {
            try
            {
                if (email.IsNullOrWriteSpace())
                {
                    return false;
                }
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static List<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        public static bool IsValidPassword(this string passWord)
        {
            var match = Regex.Match(passWord, @"(?=^.{6,}$)(?=.*\d)(?=.*[a-zA-Z])", RegexOptions.None);
            return match.Success;
        }

        public static bool IsNullOrWriteSpace(this String text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsNotNullOrWriteSpace(this String text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        public static string EmptyIfNull(this object value)
        {
            if (value == null)
                return "";
            return value.ToString();
        }

        #endregion Char and string

        #region List and emum

        public static bool IsInIgnoreCase(this string str, List<string> list)
        {
            var result = false;
            if (list.IsNotEmpty() && str != null)
            {
                result = list.FirstOrDefault(t => t.Equals(str, StringComparison.CurrentCultureIgnoreCase)) != null;
            }
            return result;
        }

        public static bool In<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException("source");
            return list.Contains(source);
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> list) => list != null && list.Any();

        public static bool IsEmpty<T>(this IEnumerable<T> list) => list == null || !list.Any();

        public static void SpecialAddRange<T>(this ICollection<T> list, IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                list.Add(item);
            }
        }

        public static string GetDisplayName<T>(this T enumValue) where T : struct
        {
            var type = enumValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("must be of enum type", nameof(enumValue));
            }

            var enumString = enumValue.ToString() ?? string.Empty;
            var member = type.GetMember(enumString);
            if (member != null && member.Length > 0)
            {
                var attributeData = member[0].GetCustomAttributesData().FirstOrDefault();
                if (attributeData != null)
                {
                    return attributeData.NamedArguments.FirstOrDefault().TypedValue.Value!.ToString() ?? string.Empty;
                }
            }

            return enumString;
        }
        #endregion List and emum

        #region Distance, map

        public static double GetRad(double x)
        {
            return x * Math.PI / 180;
        }

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            var R = 6378137; // Earth’s mean radius in meter
            var dLat = GetRad(lat2 - lat1);
            var dLong = GetRad(lng2 - lng1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(GetRad(lat1)) * Math.Cos(GetRad(lat2)) *
              Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d; // returns the distance in meter
        }

        #endregion Distance, map

        #region Http

        public static string GetHeader(this HttpContext context, string headerName)
        {
            var result = string.Empty;
            var rawData = context.Request.Headers[headerName];
            if (rawData.IsNotEmpty())
            {
                result = rawData.ToString();
            }
            return result;
        }

        #endregion Http

        #region datetime and timezone

        public static DateTime? StartOfDate(this DateTime? date)
        {
            DateTime? result = null;
            if (date != null)
            {
                result = date.Value.StartOfDate();
            }
            return result;
        }

        public static DateTime? EndOfDate(this DateTime? date)
        {
            DateTime? result = null;
            if (date != null)
            {
                result = date.Value.EndOfDate();
            }
            return result;
        }

        public static DateTime EndOfDate(this DateTime date)
        {
            return date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
        }

        public static DateTime StartOfDate(this DateTime date)
        {
            return date.AddHours(0).AddMinutes(0).AddSeconds(0).AddMilliseconds(0);
        }

        public static string ToISOString(this DateTime date) => date.ToString("o");

        public static string ToISOString(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToISOString() : string.Empty;
        }

        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public static DateTime GetFirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static bool IsFuture(this DateTime date, DateTime from)
        {
            return date.Date > from.Date;
        }

        public static bool IsFuture(this DateTime date)
        {
            return date.IsFuture(DateTime.Now);
        }

        public static bool IsPast(this DateTime date, DateTime from)
        {
            return date.Date < from.Date;
        }

        public static bool IsPast(this DateTime date)
        {
            return date.IsPast(DateTime.Now);
        }

        public static DateTime ConvertFromTimestamp(this double value)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(value).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime ConvertFromTimestamp(this string value)
        {
            double time;
            double.TryParse(value, out time);
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(time).ToLocalTime();
            return dtDateTime;
        }

        public static double ConvertToTimestamp(this DateTime value)
        {
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return Math.Truncate(span.TotalSeconds);
        }

        public static string ToYYYYMMDDString(this DateTime date)
        {
            return date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
        }

        #endregion datetime and timezone

        #region number, bool and parse

        public static double TryParseDouble(this object input)
        {
            double d;
            if (input is IConvertible)
            {
                d = ((IConvertible)input).ToDouble(null);
            }
            else
            {
                d = 0d;
            }
            return d;
        }

        public static int? ToNullableInt(this string s)
        {
            if (int.TryParse(s, out int i)) return i;
            return null;
        }

        public static int TryParseInt(this string value, int defaultVal = 0)
        {
            var result = defaultVal;
            if (value.IsNotEmpty())
            {
                int.TryParse(value, out result);
            }
            return result;
        }

        public static double TryParseDouble(this string value, double defaultVal = 0)
        {
            var result = defaultVal;
            if (value.IsNotEmpty())
            {
                double.TryParse(value, out result);
            }
            return result;
        }

        public static bool IsPositive(this int? input)
        {
            return input.HasValue && input.Value == 1;
        }

        public static bool IsPositive(this double? input)
        {
            return input.HasValue && input.Value == 1;
        }

        public static bool IsPositive(this bool? input)
        {
            return input.HasValue && input.Value;
        }

        public static bool IsNegative(this int? input)
        {
            return !input.HasValue || input.Value == 0;
        }

        public static bool IsNegative(this double? input)
        {
            return !input.HasValue || input.Value == 0;
        }

        public static bool IsNegative(this bool? input)
        {
            return !input.HasValue || !input.Value;
        }

        public static bool IsNullOrEmpty(this double? input)
        {
            return !input.HasValue || input.Value.ToString().IsNullOrWriteSpace();
        }

        #endregion number, bool and parse

        #region Stream and file

        public static MemoryStream Clone(this MemoryStream ms)
        {
            var pos = 0; //New memory stream return from 0
            var ms2 = new MemoryStream();
            ms.Position = 0;
            CopyStream(ms, ms2);
            ms2.Position = pos;
            return ms2;
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public static string GetString(this byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
                return "";

            // Ansi as default
            Encoding encoding = Encoding.Default;

            /*
            EF BB BF        UTF-8
            FF FE UTF-16    little endian
            FE FF UTF-16    big endian
            FF FE 00 00     UTF-32, little endian
            00 00 FE FF     UTF-32, big-endian
            */
            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                encoding = Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                encoding = Encoding.Unicode;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                encoding = Encoding.BigEndianUnicode; // utf-16be
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
                encoding = Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
                encoding = Encoding.UTF8;
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static string ConvertToBase64(this Stream stream)
        {
            var bytes = new Byte[(int)stream.Length];

            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);

            return Convert.ToBase64String(bytes);
        }

        #endregion Stream and file

        #region Base64

        public static bool IsBase64String(this string base64)
        {
            var strArr = base64.Split("base64,");
            if (strArr.Count() <= 1)
            {
                return false;
            }
            var base64Str = strArr[1];
            Span<byte> buffer = new Span<byte>(new byte[base64Str.Length]);
            var isBase64 = Convert.TryFromBase64String(base64Str, buffer, out int _);
            return isBase64;
        }

        public static bool IsBase64StringNormal(this string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            var isBase64 = Convert.TryFromBase64String(base64, buffer, out int _);
            return isBase64;
        }

        #endregion Base64
    }
}