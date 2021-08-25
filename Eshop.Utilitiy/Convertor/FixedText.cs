using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Eshop.Utilitiy.Convertor
{
    public static class FixedText
    {
        public static string ConvertBrToNewLine(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Replace("<br/>", Environment.NewLine);
        }


        public static string ConvertNewLineToBr(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Replace(Environment.NewLine, "<br/>");
        }


        public static string FixedEmail(string email)
        {
            return email.Trim().ToLower();
        }


        public static string[] SplitTags(string tags)
        {
            return tags.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ReplaceBr(string text)
        {
            return text.Replace("<br/>", " . ");
        }

        public static string MergTags(string[] tags)
        {
            return string.Join(",", tags);
        }

        public static string FixetTextForUrl(string text)
        {
            string url= HttpUtility.UrlEncode(text).Trim().Replace("+","-");
            if (url.EndsWith("-"))
            {
                url= url.Substring(0, url.Length - 1);
            }
            if (url.StartsWith("-"))
            {
                url = url.Substring(1, url.Length-1);
            }
            return url;
        }

        public static string RemoveHtmlInText(string html)
        {
            html = html.Replace("<br/>", ".");
            return Regex.Replace(html, "<.*?>", string.Empty);
        }

        public static string GetCharaterwidthLenght(string text, int lenght)
        {
            string result = RemoveHtmlInText(text);
            if (result.Length>=(lenght-1))
            return result.Substring(0, lenght);

            return result;
        }
    }
}

