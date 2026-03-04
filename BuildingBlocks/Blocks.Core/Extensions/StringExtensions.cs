namespace Blocks.Core.Extensions;

public static class StringExtensions
{
    public static string FormatWith(this string @this, params object[] args)
        => string.Format(@this, args);

    public static string FormatWith(this string @this, object args)
        => string.Format(@this, args);
}
