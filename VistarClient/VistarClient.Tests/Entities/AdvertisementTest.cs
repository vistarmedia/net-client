using System;
using System.Net;
using NUnit.Framework;
using Rhino.Mocks;
using VistarClient.Entities;
using VistarClient.Request;
using VistarClient.Utils;

namespace VistarClient.Tests.Entities {
  [TestFixture]
  public class AdvertisementTest {

    [Test]
    public void SendProofOfPlay() {
      var mockery = new MockRepository();

      var requestFactory = mockery.StrictMock<IVistarWebRequestFactory>();
      var request = mockery.StrictMock<IVistarWebRequest>();
      var response = mockery.DynamicMock<WebResponse>();
      var advertisement = mockery.PartialMock<Advertisement>(requestFactory);

      using (mockery.Record()) {
        Expect.Call(advertisement.ProofOfPlayUrl).Return("http://example.com");
        Expect.Call(requestFactory.Create("http://example.com")).Return(request);
        Expect.Call(request.Get()).Return(response);
      }

      using (mockery.Playback()) {
        advertisement.SendProofOfPlay();
      }
    }

    [Test]
    public void SendProofOfPlay_Throws_InvalidLeaseException_When_BadRequest() {
      var mockery = new MockRepository();

      var requestFactory = mockery.StrictMock<IVistarWebRequestFactory>();
      var request = mockery.StrictMock<IVistarWebRequest>();
      var advertisement = mockery.PartialMock<Advertisement>(requestFactory);

      var exception = new VistarWebException(new WebException(), HttpStatusCode.BadRequest);

      using (mockery.Record()) {
        Expect.Call(advertisement.ProofOfPlayUrl).Return("http://example.com");
        Expect.Call(requestFactory.Create("http://example.com")).Return(request);
        Expect.Call(request.Get()).Throw(exception);
      }

      using (mockery.Playback()) {
        Assert.Throws(typeof(InvalidLeaseException), () => {
          advertisement.SendProofOfPlay();
        });
      }
    }

    [Test]
    public void SendProofOfPlay_Throws_ApiException_When_Error_NotBadRequest() {
      var mockery = new MockRepository();

      var requestFactory = mockery.StrictMock<IVistarWebRequestFactory>();
      var request = mockery.StrictMock<IVistarWebRequest>();
      var advertisement = mockery.PartialMock<Advertisement>(requestFactory);

      var exception = new VistarWebException(new WebException(), HttpStatusCode.RequestTimeout);

      using (mockery.Record()) {
        Expect.Call(advertisement.ProofOfPlayUrl).Return("http://example.com");
        Expect.Call(requestFactory.Create("http://example.com")).Return(request);
        Expect.Call(request.Get()).Throw(exception);
      }

      using (mockery.Playback()) {
        Assert.Throws(typeof(ApiException), () => {
          advertisement.SendProofOfPlay();
        });
      }
    }

    [Test]
    public void SendProofOfPlay_WithData() {
      var mockery = new MockRepository();

      var requestFactory = mockery.StrictMock<IVistarWebRequestFactory>();
      var request = mockery.StrictMock<IVistarWebRequest>();
      var response = mockery.DynamicMock<WebResponse>();
      var advertisement = mockery.PartialMock<Advertisement>(requestFactory);

      var displayTime = new DateTime(2013, 4, 23);
      var expectedJson =
        string.Format("{{\"display_time\": {0}}}",
          displayTime.ToUtcUnixTime());

      using (mockery.Record()) {
        Expect.Call(advertisement.ProofOfPlayUrl).Return("My URL");
        Expect.Call(requestFactory.Create("My URL")).Return(request);
        Expect.Call(request.Post(expectedJson)).Return(response);
      }

      using (mockery.Playback()) {
        advertisement.SendProofOfPlay(
          new ProofOfPlayMessage(requestFactory) {
            DisplayTime = displayTime
          }
        );
      }
    }

    [Test]
    public void SendExpiration() {
      var mockery = new MockRepository();

      var requestFactory = mockery.StrictMock<IVistarWebRequestFactory>();
      var request = mockery.StrictMock<IVistarWebRequest>();
      var response = mockery.DynamicMock<WebResponse>();
      var advertisement = mockery.PartialMock<Advertisement>(requestFactory);

      using (mockery.Record()) {
        Expect.Call(advertisement.ExpirationUrl).Return("http://example.com");
        Expect.Call(requestFactory.Create("http://example.com")).Return(request);
        Expect.Call(request.Get()).Return(response);
      }

      using (mockery.Playback()) {
        advertisement.SendExpiration();
      }
    }

    [Test]
    public void SendExpiration_Throws_InvalidLeaseException_When_BadRequest() {
      var mockery = new MockRepository();

      var requestFactory = mockery.StrictMock<IVistarWebRequestFactory>();
      var request = mockery.StrictMock<IVistarWebRequest>();
      var advertisement = mockery.PartialMock<Advertisement>(requestFactory);

      var exception = new VistarWebException(new WebException(), HttpStatusCode.BadRequest);

      using (mockery.Record()) {
        Expect.Call(advertisement.ExpirationUrl).Return("http://example.com");
        Expect.Call(requestFactory.Create("http://example.com")).Return(request);
        Expect.Call(request.Get()).Throw(exception);
      }

      using (mockery.Playback()) {
        Assert.Throws(typeof(InvalidLeaseException), () => {
          advertisement.SendExpiration();
        });
      }
    }

    [Test]
    public void SendExpiration_Throws_ApiException_When_Error_NotBadRequest() {
      var mockery = new MockRepository();

      var requestFactory = mockery.StrictMock<IVistarWebRequestFactory>();
      var request = mockery.StrictMock<IVistarWebRequest>();
      var advertisement = mockery.PartialMock<Advertisement>(requestFactory);

      var exception = new VistarWebException(new WebException(), HttpStatusCode.RequestTimeout);

      using (mockery.Record()) {
        Expect.Call(advertisement.ExpirationUrl).Return("http://example.com");
        Expect.Call(requestFactory.Create("http://example.com")).Return(request);
        Expect.Call(request.Get()).Throw(exception);
      }

      using (mockery.Playback()) {
        Assert.Throws(typeof(ApiException), () => {
          advertisement.SendExpiration();
        });
      }
    }
  }
}
