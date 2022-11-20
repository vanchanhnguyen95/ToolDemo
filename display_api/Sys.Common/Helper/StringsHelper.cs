using Sys.Common.Logs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Sys.Common.Helper
{
    public static class StringsHelper
    {
        private static nProxLog log = new nProxLog();

        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string SHA1Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public static long TimeStamp()
        {
            DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            DateTime foo = DateTime.UtcNow;
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            return unixTime;
        }

        private static readonly string[] VietnameseSigns = new string[]
         {
            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
         };

        public static readonly string[] ChangeVietnameseSigns = new string[]
        {
            "aáàạảãâấầậẩẫăắằặẳẵ",

            "AÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "eéèẹẻẽêếềệểễ",

            "EÉÈẸẺẼÊẾỀỆỂỄ",

            "oóòọỏõôốồộổỗơớờợởỡ",

            "OÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "uúùụủũưứừựửữ",

            "UÚÙỤỦŨƯỨỪỰỬỮ",

            "iíìịỉĩ",

            "IÍÌỊỈĨ",

            "yýỳỵỷỹ",

            "YÝỲỴỶỸ"
        };

        public static int? LocationOfNumberInString(this string text)
        {
            int? result = null;
            try
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (Char.IsDigit(text[i]))
                    {
                        result = i;
                        if (!Char.IsDigit(text[i + 1]) && Char.IsDigit(text[i - 1]))
                        {
                            result += 1;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Log(LogConstants.LogType.Error, ex.Message);
            }
            return result;
        }

        public static string RemoveSign4VietnameseString(this string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        public static string PascalCase(this string para)
        {
            para = para.ToLower().Trim();
            return string.IsNullOrEmpty(para) ? null : para.FirstCharToUpper();
        }

        public static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        public static bool ContainsAny(this string haystack, params string[] needles)
        {
            foreach (string needle in needles)
            {
                if (haystack.ToLower().Contains(needle.ToLower()))
                    return true;
            }

            return false;
        }

        #region Generate Invite code

        private static readonly Random random = new Random();

        public static string GenerateInviteCode(string prefix = "", int? length = 8)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var secondChard = "0123456789";
            var result = new string(
                Enumerable.Repeat(chars, (int)length - prefix.Length - 1)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            var secondResult = new string(Enumerable.Repeat(secondChard, 1)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return string.Format("{0}{1}{2}", prefix.Trim(), result, secondResult);
        }

        public static String ReduceWhitespace(this String value)
        {
            var newString = new StringBuilder();
            bool previousIsWhitespace = false;
            for (int i = 0; i < value.Length; i++)
            {
                if (Char.IsWhiteSpace(value[i]))
                {
                    if (previousIsWhitespace)
                    {
                        continue;
                    }
                    previousIsWhitespace = true;
                }
                else
                {
                    previousIsWhitespace = false;
                }
                newString.Append(value[i]);
            }
            return newString.ToString();
        }

        public static bool IsPhoneNumberVietnam(this string phoneNumber)
        {
            string pattern = @"(84|0[3|5|7|8|9])+([0-9]{8})\b";
            Regex rg = new Regex(pattern);
            return rg.Match(phoneNumber).Success;
        }

        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        #endregion Generate Invite code

        #region Generate Passcode

        public static string GeneratePasscode(int length = 6)
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion Generate Passcode

        #region Generate Open with app link

        public static string GenerateOpenWithAppLink()
        {
            return "this_is_fake_link_for_testing_perpose";
        }

        #endregion Generate Open with app link

        #region Active invitation and more

        public static string Chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static string Base10To62(double input)
        {
            string result = "";
            var DataInlong = Convert.ToInt64(input);
            do { result += Chars[(int)(DataInlong % 0x3E)]; } while ((DataInlong /= 0x3E) != 0);
            return result;
        }

        private static double Base62To10(string input)
        {
            double result = 0;
            int L = input.Length;
            for (int i = 0; i < L; i++) result += Chars.IndexOf(input[i]) * (long)(System.Math.Pow(0x3E, i));
            return result;
        }

        public static string Encode(List<double> data, string seperator = "&0g")
        {
            var result = "";
            foreach (var item in data)
            {
                var encypString = Base10To62(item);
                result += encypString + seperator;
            }
            return result.TrimEnd(seperator.ToCharArray());
        }

        public static List<double> Decode(string input, string sperator = "&0g")
        {
            List<double> result = new List<double>();
            var data = input.Split(sperator);
            foreach (var item in data)
            {
                result.Add(Base62To10(item));
            }
            return result;
        }

        #endregion Active invitation and more

        #region Image

        public static bool IsImageExtension(this string ext)
        {
            string[] _validExtensions = { ".jpg", ".bmp", ".gif", ".png", ".jfif", ".tiff", ".pdf", ".apng", ".avif", ".pjpeg", ".pjp", ".svg", ".webp", ".ico", ".cur", ".tif", ".tiff" };
            return _validExtensions.Contains(ext);
        }

        #endregion Image
    }
}