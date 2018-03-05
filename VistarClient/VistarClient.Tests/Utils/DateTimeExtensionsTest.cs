using System;
using NUnit.Framework;
using VistarClient.Utils;

namespace VistarClient.Tests.Utils {
  [TestFixture]
  public class DateTimeExtensionsTest {
    [Test]
    public void FromUnixTime() {
      var expected = new DateTime(2012, 4, 12, 9, 37, 0, DateTimeKind.Utc);
      const long unixTime = 1334237820;

      Assert.AreEqual(expected, new DateTime().GetLocalFromUtcUnixTime(unixTime));
    }

    [Test]
    public void ToUnixTime() {
      const long expected = 1334237820;
      var dateTime = new DateTime(2012, 4, 12, 9, 37, 0, DateTimeKind.Local);

      Assert.AreEqual(expected, dateTime.ToUtcUnixTime());
    }

    [Test]
    public void StripSeconds() {
      var expected = new DateTime(2012, 4, 12, 9, 37, 0);
      var dateTime = new DateTime(2012, 4, 12, 9, 37, 24);

      Assert.AreEqual(expected, dateTime.StripSeconds());
    }
  }
}
