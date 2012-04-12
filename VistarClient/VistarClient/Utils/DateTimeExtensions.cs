using System;

namespace VistarClient.Utils {
  public static class DateTimeExtensions {
    static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static DateTime GetLocalFromUtcUnixTime(this DateTime dateTime, long unixTime) {
      return epoch.AddSeconds(unixTime).ToLocalTime();
    }

    public static DateTime StripSeconds(this DateTime dateTime) {
      return dateTime.AddSeconds(-(dateTime.Second));
    }

    public static long ToUtcUnixTime(this DateTime dateTime) {
      if (dateTime.Kind != DateTimeKind.Utc) {
        dateTime = dateTime.ToUniversalTime();
      }

      return (long)Math.Floor((dateTime - epoch).TotalSeconds);
    }
  }
}

