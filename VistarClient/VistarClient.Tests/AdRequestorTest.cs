using System;
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

      var ad = new Advertisement();
      var ads = new List<Advertisement> { ad };
      var advertisementResponse = new AdvertisementResponse{advertisement = ads};
      var restResponse = new RestResponse<AdvertisementResponse>();
      restResponse.Data = advertisementResponse;
     
      using(mockery.Record()) {
        restRequest.RequestFormat = DataFormat.Json;
        Expect.Call(restRequest.JsonSerializer).Return(serializer);
        Expect.Call(serializer.Serialize(adRequest)).Return(str);
        Expect.Call(restRequest.AddParameter("text/json", str, ParameterType.RequestBody)).Return(new RestRequest());
        Expect.Call(restClient.Execute<AdvertisementResponse>(restRequest)).Return(restResponse);
      }
     
      using(mockery.Playback()) {
        var result = new AdRequestor(restClient, restRequest).RunSubmitAdRequest(adRequest);
        Assert.AreEqual(ads, result);
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

      using(mockery.Record()) {
        restRequest.RequestFormat = DataFormat.Json;
        Expect.Call(restRequest.JsonSerializer).Return(serializer);
        Expect.Call(serializer.Serialize(adRequest)).Return(str);
        Expect.Call(restRequest.AddParameter("text/json", str, ParameterType.RequestBody)).Return(new RestRequest());
        Expect.Call(restClient.Execute<AdvertisementResponse>(restRequest)).Throw(new Exception(error));
      }

      using(mockery.Playback()) {
        var ex = Assert.Throws(typeof(ApiException), () => {
          new AdRequestor(restClient, restRequest).RunSubmitAdRequest(adRequest);
        });

        Assert.AreEqual(error, ex.Message);
      }
    }
  }
}

