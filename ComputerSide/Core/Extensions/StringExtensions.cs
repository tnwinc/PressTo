using System;

namespace Core.Extensions
{
  public static class StringExtensions
  {
    public static string FormatWith(this string me, params object[] values) {
      return me == null ? null : String.Format(me, values);
    }

    public static bool IsNullOrEmpty(this string me) {
      return string.IsNullOrEmpty(me);
    }

    public static bool IsNullOrWhitespace(this string me) {
      return string.IsNullOrWhiteSpace(me);
    }
  }
}
