using System;
using System.Net;
using NUnit.Framework;
using Rhino.Mocks;
using VistarClient.Entities;
using VistarClient.Utils;

namespace VistarClient.Tests {
 [TestFixture]
  public class AdvertisementTest {
   
   [Test]
    public void SendProofOfPlay() {
      var mockery = new MockRepository();

      var requestFactory = mockery.StrictMock<IVistarWebRequestFactory>();
      var request = mockery.StrictMock<IVistarWebRequest>();
      var response = mockery.PartialMock<WebResponse>();

      var advertisement = new Advertisement(requestFactory) {
        ProofOfPlayUrl = "http://test.url/proof_of_play.html"
      };

      using(mockery.Record()) {
        Expect.Call(requestFactory.Create(advertisement.ProofOfPlayUrl)).Return(request);
        Expect.Call(request.GetResponse()).Return(response);
      }

      using(mockery.Playback()) {
        advertisement.SendProofOfPlay();
      }
    }
   
   [Test]
    public void SendProofOfPlay_Throws_InvalidLeaseException_When_BadRequest() {
      var mockery = new MockRepository();

      var requestFactory = mockery.StrictMock<IVistarWebRequestFactory>();
      var request = mockery.StrictMock<IVistarWebRequest>();

      var exception = new VistarWebException(new WebException(), HttpStatusCode.BadRequest);

      var advertisement = new Advertisement(requestFactory) {
        ProofOfPlayUrl = "http://test.url/proof_of_play.html"
      };

      using(mockery.Record()) {
        Expect.Call(requestFactory.Create(advertisement.ProofOfPlayUrl)).Return(request);
        Expect.Call(request.GetResponse()).Throw(exception);
      }

      using(mockery.Playback()) {
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

      var exception = new VistarWebException(new WebException(), HttpStatusCode.RequestTimeout);

      var advertisement = new Advertisement(requestFactory) {
        ProofOfPlayUrl = "http://test.url/proof_of_play.html"
      };

      using(mockery.Record()) {
        Expect.Call(requestFactory.Create(advertisement.ProofOfPlayUrl)).Return(request);
        Expect.Call(request.GetResponse()).Throw(exception);
      }

      using(mockery.Playback()) {
        Assert.Throws(typeof(ApiException), () => {
          advertisement.SendProofOfPlay();
        });
      }
    }
  }
}

