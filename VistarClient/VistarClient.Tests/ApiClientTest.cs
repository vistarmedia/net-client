using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serializers;
using Rhino.Mocks;
using VistarClient.Entities;
using VistarClient.Request;

namespace VistarClient.Tests {
 [TestFixture]
  public class ApiClientTest {

   [Test]
    public void SubmitAdRequest_Success() {
      var mockery = new MockRepository();
      var restClient = mockery.StrictMock<IRestClient>();
      var restRequest = mockery.StrictMock<IRestRequest>();
      var serializer = mockery.StrictMock<ISerializer>();
     
      var adRequest = new AdRequest();
      var str = "serialized data";
     
      var ad = new Advertisement();
      var advertisementResponse = new AdvertisementResponse{Advertisements = new List<Advertisement>{ad}};
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
        var client = new ApiClient(restClient, restRequest);
        var rtn = client.SubmitAdRequest(adRequest);
        Assert.AreSame(ad, rtn);
      }
    }
   
   [Test]
    public void SubmitAdRequest_Throws_ApiException_When_Error() {
      var mockery = new MockRepository();
      var restClient = mockery.StrictMock<IRestClient>();
      var restRequest = mockery.DynamicMock<IRestRequest>();
      var serializer = mockery.Stub<ISerializer>();
     
      var error = "Test error message";
     
      using(mockery.Record()) {
        SetupResult.For(restRequest.JsonSerializer).Return(serializer);
        Expect.Call(restClient.Execute<AdvertisementResponse>(restRequest)).Throw(new Exception(error));
      }
     
      using(mockery.Playback()) {
        var ex = Assert.Throws(typeof(ApiException), () => {
          new ApiClient(restClient, restRequest).SubmitAdRequest(new AdRequest());
        });
       
        Assert.AreEqual(error, ex.Message);
      }
    }
  }
}

