using System.Text;
using System.Text.RegularExpressions;

namespace CommonHelpers
{
    public static class RegexHelper
    {
        public static string GetContent(string Input,int Max)
        {
            string pattern = "[\u4E00-\u9FA5A-Za-z0-9_ .]";
            string replacement = "";
            StringBuilder result = new StringBuilder();
            foreach (Match match in Regex.Matches(Input, pattern))
            {
                result.Append(match.Value);
                if (result.Length == Max) break;
            }
            return result.ToString();
        }
    }
}
