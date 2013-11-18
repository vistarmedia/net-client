using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using VistarClient.Entities;
using VistarClient.Request;
using VistarClient.Utils;

namespace VistarClient {
  public interface IAdRequestor {
    List<Advertisement> RunSubmitAdRequest(AdRequest request);
  }

  public sealed class AdRequestor : RestService, IAdRequestor {
    const string RESOURCE = "/api/v1/get_ad/json";

    public AdRequestor()
      : this(new RestClient(GetHost()), new RestRequestFactory()) {

    }

    public AdRequestor(IRestClient restClient,
      IRestRequestFactory requestFactory)
      : base(restClient, requestFactory) {

    }

    public List<Advertisement> RunSubmitAdRequest(AdRequest request) {
      var restRequest = requestFactory.Create(RESOURCE, METHOD);
      restRequest.RequestFormat = DataFormat.Json;
      string data = restRequest.JsonSerializer.Serialize(request.ToMessage());
      restRequest.AddParameter("text/json", data, ParameterType.RequestBody);

      try {
        var response = restClient
          .Execute<AdvertisementResponseMessage>(restRequest);
        var ads = new List<Advertisement>();
        if (response.Data == null && response.ErrorMessage != null) {
          if (VistarGlobals.IsDebug) {
            Console.WriteLine("Raw Response: {0}", response.Content);
          }
          throw new ApiException("AdRequest Failed: " + response.ErrorMessage);
        }
        else if (response.Data == null) {
          if (VistarGlobals.IsDebug) {
            Console.WriteLine("Raw Response: {0}", response.Content);
          }
          throw new ApiException("AdRequest Failed.");
        }

        if (response.Data.advertisement != null) {
          ads = response.Data.advertisement
            .Select(Advertisement.FromMessage).ToList();
        }
        return ads;
      }
      catch (Exception ex) {
        throw new ApiException(ex.Message);
      }
    }

    internal static string GetHost() {
      var host =
        System.Configuration.ConfigurationManager.AppSettings["ApiHost"];

      if (host != null) {
        return string.Format("http://{0}", host);
      }

      throw new ApiException(
        "You must specify an ApiHost in your configuration file.");
    }
  }
}
