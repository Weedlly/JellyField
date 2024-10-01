using System.Text.RegularExpressions;

namespace Common.Scripts.Utilities
{
    public class StringUtility
    {
        public static string PrepareConvertIntValue(string str)
        {
            return Regex.Replace(str, @"[^a-zA-Z0-9 ]", "");
        }
    }
}
