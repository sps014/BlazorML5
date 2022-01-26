namespace BlazorML5.Helpers;

internal static class UtilHelper
{
    public static string FirstCharSmall(string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;
        return str.Substring(0, 1).ToLower() + str.Substring(1);
    }
}