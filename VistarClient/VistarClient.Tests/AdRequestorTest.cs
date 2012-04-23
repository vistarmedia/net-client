using System;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using RestSharp;
using VistarClient.Entities;
using VistarClient.Request;
using System.Collections.Generic;
using RestSharp.Serializers;

namespace VistarClient.Tests {
  [TestFixture]
  public class AdRequestorTest {
    [Test]
    public void RunSubmitAdRequest() {
      var mockery = new MockRepository();
      var restClient = mockery.StrictMock<IRestClient>();
      var restRequest = mockery.StrictMock<IRestRequest>();
      var serializer = mockery.Stub<ISerializer>();

      var str = "some data";

      var adRequest = new AdRequest();

      var ad = new AdvertisementMessage { id = "test" };
      var ads = new List<AdvertisementMessage> { ad };
      var advertisementResponse = new AdvertisementResponseMessage { advertisement = ads };
      var restResponse = new RestResponse<AdvertisementResponseMessage>();
      restResponse.Data = advertisementResponse;

      using (mockery.Record()) {
        restRequest.RequestFormat = DataFormat.Json;
        Expect.Call(restRequest.JsonSerializer).Return(serializer);
        Expect.Call(serializer.Serialize(adRequest)).Return(str);
        Expect.Call(restRequest.AddParameter("text/json", str, ParameterType.RequestBody)).Return(new RestRequest());
        Expect.Call(restClient.Execute<AdvertisementResponseMessage>(restRequest)).Return(restResponse);
      }

      using (mockery.Playback()) {
        var results = new AdRequestor(restClient, restRequest).RunSubmitAdRequest(adRequest);
        Assert.AreEqual(ad.id, results[0].Id);
      }
    }

    [Test]
    public void RunSubmitAdRequest_Throws_ApiException_When_Error() {
      var mockery = new MockRepository();
      var restClient = mockery.StrictMock<IRestClient>();
      var restRequest = mockery.DynamicMock<IRestRequest>();
      var serializer = mockery.Stub<ISerializer>();

      var str = "some data";

      var error = "Test error message";
      var adRequest = new AdRequest();

      using (mockery.Record()) {
        restRequest.RequestFormat = DataFormat.Json;
        Expect.Call(restRequest.JsonSerializer).Return(serializer);
        Expect.Call(serializer.Serialize(adRequest)).Return(str);
        Expect.Call(restRequest.AddParameter("text/json", str, ParameterType.RequestBody)).Return(new RestRequest());
        Expect.Call(restClient.Execute<AdvertisementResponseMessage>(restRequest)).Throw(new Exception(error));
      }

      using (mockery.Playback()) {
        var ex = Assert.Throws(typeof(ApiException), () => {
          new AdRequestor(restClient, restRequest).RunSubmitAdRequest(adRequest);
        });

        Assert.AreEqual(error, ex.Message);
      }
    }
  }
}

