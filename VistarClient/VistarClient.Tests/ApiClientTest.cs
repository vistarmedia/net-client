using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serializers;
using Rhino.Mocks;
using VistarClient.Entities;
using VistarClient.Request;
using VistarClient.Utils;

namespace VistarClient.Tests.Request {
 [TestFixture]
  public class ApiClientTest {

    [Test]
    public void SubmitAdRequest_Success() {
      var mockery = new MockRepository();
      var adRequestor = mockery.StrictMock<IAdRequestor>();
     
      var adRequest = new AdRequest();
     
      var ad = new Advertisement();
      var ads = new List<Advertisement> { ad };
     
      using(mockery.Record()) {
        Expect.Call(adRequestor.RunSubmitAdRequest(adRequest)).Return(ads);
      }
     
      using(mockery.Playback()) {
        var client = new ApiClient(adRequestor);
        var rtn = client.SubmitAdRequest(adRequest);
        Assert.AreEqual(ad, rtn);
      }
    }
  }
}

