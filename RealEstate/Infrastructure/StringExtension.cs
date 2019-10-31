namespace RealEstate.Infrastructure
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Text.RegularExpressions;

    public static class StringExtension
    {
        #region Converter
        private static string ConvertInternationalCharToASCII(char character)
        {
            string s = character.ToString().ToLowerInvariant();

            if ("àåáâäãåąā".Contains(s))
                return "a";
            else if ("èéêëę".Contains(s))
                return "e";
            else if ("ìíîïı".Contains(s))
                return "i";
            else if ("òóôõöøőð".Contains(s))
                return "o";
            else if ("ùúûüŭů".Contains(s))
                return "u";
            else if ("çćčĉ".Contains(s))
                return "c";
            else if ("żźž".Contains(s))
                return "z";
            else if ("śşšŝ".Contains(s))
                return "s";
            else if ("ñń".Contains(s))
                return "n";
            else if ("ýÿ".Contains(s))
                return "y";
            else if ("ğĝ".Contains(s))
                return "g";
            else if (s == "ř")
                return "r";
            else if (s == "ł")
                return "l";
            else if (s == "đ")
                return "d";
            else if (s == "ß")
                return "ss";
            else if (s == "Þ")
                return "th";
            else if (s == "ĥ")
                return "h";
            else if (s == "ĵ")
                return "j";
            else
                return string.Empty;
        }
        #endregion

        public static bool IsNullOrEmpty(this string content)
            => string.IsNullOrEmpty(content);

        public static string SubBetween(this string content, string leftContent, string rightContent)
            => content.SubBefore(rightContent).SubAfter(leftContent);

        public static string SubBefore(this string conntent, string pattern)
        {
            int index = conntent.IndexOf(pattern);
            return (index == -1) ? string.Empty : conntent.Substring(0, index);
        }

        public static string SubAfter(this string content, string pattern)
        {
            int start = content.LastIndexOf(pattern) + pattern.Length;
            return start >= content.Length ? string.Empty : content.Substring(start).Trim();
        }

        public static string MaxLength(this string content, int length = 40)
            => string.IsNullOrEmpty(content) ? string.Empty :
               content.Length > length ? $"{content.Substring(0, length - 3)}..." : content;

        public static bool IsUri(this string uriName)
        {
            Uri uriResult;
            return Uri.TryCreate(uriName, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public static string UTF8toASCII(this string utf8)
            => new Regex("\\p{IsCombiningDiacriticalMarks}+")
            .Replace(utf8.Normalize(NormalizationForm.FormD), string.Empty)
            .Replace('\u0111', 'd').Replace('\u0110', 'D');

        public static string TitleCase(this string title)
            => Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(title);

        public static string SingleSpace(this string input)
             => Regex.Replace(input, "\\s+", " ");

        public static string RewriteUrl(this string url, int maxLength = 80, bool isConvertToASCII = true, bool isLower = true, params char[] except)
        {
            if (url == null)
                return string.Empty;
            url = url.Normalize(NormalizationForm.FormKD);

            int length = url.Length;
            int lengthBuilder = 0;
            bool isHyphen = false;
            StringBuilder builder = new StringBuilder(Math.Min(length, maxLength + 1));

            char characters;
            for (int i = 0; i < length; ++i)
            {
                characters = url[i];
                if ((characters >= 'a' && characters <= 'z') || (characters >= '0' && characters <= '9'))
                {
                    builder.Append(characters);
                    isHyphen = false;
                }
                else if (characters >= 'A' && characters <= 'Z')
                {
                    builder.Append(isLower ? (char)(characters | 32) : characters);
                    isHyphen = false;
                }
                else if (!except.Contains(characters) && ((characters == ' ') || (characters == ',') || (characters == '.') || (characters == '/') ||
                    (characters == '\\') || (characters == '_') || (characters == '=')))
                {
                    lengthBuilder = builder.Length;
                    if (lengthBuilder > 0)
                        if (!isHyphen)
                        {
                            builder.Append('-');
                            isHyphen = true;
                        }
                }
                else if (characters >= 128)
                {
                    if (isConvertToASCII && !except.Contains(characters))
                        builder.Append(ConvertInternationalCharToASCII(characters));
                    else
                        builder.Append(characters);
                    isHyphen = false;
                }
                else if (except.Contains(characters))
                    builder.Append(characters);

                if (i == maxLength)
                    break;
            }

            return isHyphen ? builder.ToString().Substring(0, builder.Length - 1) : builder.ToString();
        }
    }
}