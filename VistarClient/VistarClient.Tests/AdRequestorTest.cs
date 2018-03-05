using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serializers;
using Rhino.Mocks;
using Is = Rhino.Mocks.Constraints.Is;
using VistarClient.Entities;
using VistarClient.Request;

namespace VistarClient.Tests {
  [TestFixture]
  public class AdRequestorTest {
    [Test]
    public void RunSubmitAdRequest() {
      var mockery = new MockRepository();
      var restClient = mockery.StrictMock<IRestClient>();
      var restRequestFactory = mockery.StrictMock<IRestRequestFactory>();
      var restRequest = mockery.StrictMock<IRestRequest>();
      var serializer = mockery.Stub<ISerializer>();

      var str = "some data";

      var adRequest = new AdRequest { NetworkId = Guid.NewGuid().ToString() };

      var ad = new AdvertisementMessage { id = "test" };
      var ads = new List<AdvertisementMessage> { ad };
      var advertisementResponse = new AdvertisementResponseMessage { advertisement = ads };
      var restResponse = new RestResponse<AdvertisementResponseMessage>();
      restResponse.Data = advertisementResponse;

      using (mockery.Record()) {
        Expect.Call(restRequestFactory.Create(null, Method.POST))
              .Constraints(Is.Anything(), Is.Equal(Method.POST))
              .Return(restRequest);
        restRequest.RequestFormat = DataFormat.Json;
        Expect.Call(restRequest.JsonSerializer).Return(serializer);
        Expect.Call(serializer.Serialize(null)).Constraints(
          Rhino.Mocks.Constraints.Property.Value("network_id", adRequest.NetworkId) &&
          Is.TypeOf<AdRequestMessage>()
        ).Return(str);
        Expect.Call(restRequest.AddParameter("text/json", str, ParameterType.RequestBody)).Return(new RestRequest());
        Expect.Call(restClient.Execute<AdvertisementResponseMessage>(restRequest)).Return(restResponse);
      }

      using (mockery.Playback()) {
        var results = new AdRequestor(restClient, restRequestFactory).RunSubmitAdRequest(adRequest);
        Assert.AreEqual(ad.id, results[0].Id);
      }
    }

    [Test]
    public void RunSubmitAdRequest_Returns_Empty_List_If_No_Ads() {
      var mockery = new MockRepository();
      var restClient = mockery.StrictMock<IRestClient>();
      var restRequestFactory = mockery.StrictMock<IRestRequestFactory>();
      var restRequest = mockery.StrictMock<IRestRequest>();
      var serializer = mockery.Stub<ISerializer>();

      var str = "some data";

      var adRequest = new AdRequest { NetworkId = Guid.NewGuid().ToString() };

      var advertisementResponse = new AdvertisementResponseMessage { advertisement = null };
      var restResponse = new RestResponse<AdvertisementResponseMessage>();
      restResponse.Data = advertisementResponse;

      using (mockery.Record()) {
        Expect.Call(restRequestFactory.Create(null, Method.POST))
              .Constraints(Is.Anything(), Is.Equal(Method.POST))
              .Return(restRequest);
        restRequest.RequestFormat = DataFormat.Json;
        Expect.Call(restRequest.JsonSerializer).Return(serializer);
        Expect.Call(serializer.Serialize(null)).Constraints(
          Rhino.Mocks.Constraints.Property.Value("network_id", adRequest.NetworkId) &&
          Is.TypeOf<AdRequestMessage>()
        ).Return(str);
        Expect.Call(restRequest.AddParameter("text/json", str, ParameterType.RequestBody)).Return(new RestRequest());
        Expect.Call(restClient.Execute<AdvertisementResponseMessage>(restRequest)).Return(restResponse);
      }

      using (mockery.Playback()) {
        var results = new AdRequestor(restClient, restRequestFactory).RunSubmitAdRequest(adRequest);
        Assert.IsEmpty(results);
      }
    }

    [Test]
    public void RunSubmitAdRequest_Throws_ApiException_When_Error() {
      var mockery = new MockRepository();
      var restClient = mockery.StrictMock<IRestClient>();
      var restRequestFactory = mockery.StrictMock<IRestRequestFactory>();
      var restRequest = mockery.DynamicMock<IRestRequest>();
      var serializer = mockery.Stub<ISerializer>();

      var str = "some data";

      var error = "Test error message";
      var adRequest = new AdRequest { NetworkId = Guid.NewGuid().ToString() };

      using (mockery.Record()) {
        Expect.Call(restRequestFactory.Create(null, Method.POST))
              .Constraints(Is.Anything(), Is.Equal(Method.POST))
              .Return(restRequest);
        restRequest.RequestFormat = DataFormat.Json;
        Expect.Call(restRequest.JsonSerializer).Return(serializer);
        Expect.Call(serializer.Serialize(null)).Constraints(
          Rhino.Mocks.Constraints.Property.Value("network_id", adRequest.NetworkId) &&
          Is.TypeOf<AdRequestMessage>()
        ).Return(str);
        Expect.Call(restRequest.AddParameter("text/json", str, ParameterType.RequestBody)).Return(new RestRequest());
        Expect.Call(restClient.Execute<AdvertisementResponseMessage>(restRequest)).Throw(new Exception(error));
      }

      using (mockery.Playback()) {
        var ex = Assert.Throws(typeof(ApiException), () => {
          new AdRequestor(restClient, restRequestFactory).RunSubmitAdRequest(adRequest);
        });

        Assert.AreEqual(error, ex.Message);
      }
    }
  }
}
