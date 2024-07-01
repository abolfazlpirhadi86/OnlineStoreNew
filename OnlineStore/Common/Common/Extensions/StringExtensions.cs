using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string Slugify(this string phrase)
        {
            var s = phrase.RemoveDiacritics().ToLower();
            s = Regex.Replace(s, @"[^\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200Fa-z0-9\s-]", ""); 
            s = Regex.Replace(s, @"\s+", " ").Trim(); 
            s = s.Substring(0, s.Length <= 100 ? s.Length : 45).Trim(); 
            s = Regex.Replace(s, @"\s", "-");         
            s = Regex.Replace(s, @"‌", "-"); 
            return s.ToLower();
        }
        public static string CleanString(this string str)
        {
            return str.Trim().ArabicToPersian().PersianToEnglish().NullIfEmpty();
        }
        public static string? NullIfEmpty(this string str)
        {
            return str?.Length == 0 ? null : str;
        }
        public static string ToCamelCase(this string input)
        {
            if (input == null || input.Length < 2)
                return input;

            string[] words = input.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            string result = words[0].ToLower();
            for (int i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }
        public static byte[] ToByteArray(this string input)
        {
            return System.Text.Encoding.UTF8.GetBytes(input);
        }
        public static string Base64Encode(this string input)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(input);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(this string input)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string AsciiToUnicode(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                for (byte i = 48; i <= 57; i++)
                {
                    str = str.Replace((char)i, (char)(i + 1728));
                }
            }
            return str;
        }
        public static string FromByteArray(this byte[] input)
        {
            return System.Text.Encoding.UTF8.GetString(input);
        }
        public static string GetSHA256Hash(this string input)
        {
            using var sha256 = SHA256.Create();
            var byteValue = Encoding.UTF8.GetBytes(input);
            var byteHash = sha256.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }
        public static string ArabicToPersian(this string str)
        {
            var arabicYeChar = (char)1610;
            var persianYeChar = (char)1740;
            var arabicKeChar = (char)1603;
            var persianKeChar = (char)1705;

            var result = str
                    .Replace(arabicYeChar, persianYeChar)
                    .Replace(arabicKeChar, persianKeChar)
                    .Replace(" ", " ")
                    .Replace("‌", " ")
                    .Replace("ھ", "ه"); 

            return result;
        }
        public static string EnglishToPersian(this string str)
        {
            return str.Replace("0", "۰")
                .Replace("1", "۱")
                .Replace("2", "۲")
                .Replace("3", "۳")
                .Replace("4", "۴")
                .Replace("5", "۵")
                .Replace("6", "۶")
                .Replace("7", "۷")
                .Replace("8", "۸")
                .Replace("9", "۹");
        }
        public static string PersianToEnglish(this string str)
        {
            return str.Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")
                //iphone numeric
                .Replace("٠", "0")
                .Replace("١", "1")
                .Replace("٢", "2")
                .Replace("٣", "3")
                .Replace("٤", "4")
                .Replace("٥", "5")
                .Replace("٦", "6")
                .Replace("٧", "7")
                .Replace("٨", "8")
                .Replace("٩", "9");
        }
        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var normalizedString = text.Normalize(NormalizationForm.FormKC);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public static List<string> SplitCamelCase(this string source)
        {
            const string pattern = @"[A-Z][a-z]*|[a-z]+|\d+";
            var matches = Regex.Matches(source, pattern);
            return matches.Select(e => e.Value.ToString()).ToList();
        }
        public static string RemoveNonAsciiCharacters(this string value)
        {
            return Regex.Replace(value, "[^ -~]", "");
        }
        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }
    }
}
