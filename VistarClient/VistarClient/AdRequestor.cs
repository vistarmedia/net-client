using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using VistarClient.Entities;
using VistarClient.Request;

namespace VistarClient {
  public interface IAdRequestor {
    List<Advertisement> RunSubmitAdRequest(AdRequest request);
  }

  public sealed class AdRequestor : IAdRequestor {
    readonly IRestClient restClient;
    readonly IRestRequestFactory requestFactory;

    internal const string RESOURCE = "api/v1/get_ad/json";
    internal const Method METHOD = Method.POST;

    static string host;

    public AdRequestor()
      : this(new RestClient(GetHost()), new RestRequestFactory()) {

    }

    public AdRequestor(IRestClient restClient, IRestRequestFactory requestFactory) {
      this.restClient = restClient;
      this.requestFactory = requestFactory;
    }

    public List<Advertisement> RunSubmitAdRequest(AdRequest request) {
      var restRequest = requestFactory.Create(RESOURCE, METHOD);
      restRequest.RequestFormat = DataFormat.Json;
      string data = restRequest.JsonSerializer.Serialize(request.ToMessage());
      restRequest.AddParameter("text/json", data, ParameterType.RequestBody);
      
      try {
        var response = restClient.Execute<AdvertisementResponseMessage>(restRequest);
        var ads = new List<Advertisement>();
        if (response.Data == null && response.ErrorMessage != null) {
          throw new ApiException("AdRequest Failed: " + response.ErrorMessage);
        }
        else if (response.Data == null) {
          throw new ApiException("AdRequest Failed.");
        }

        if (response.Data.advertisement != null) {
          ads = response.Data.advertisement.Select(Advertisement.FromMessage).ToList();
        }
        return ads;
      }
      catch (Exception ex) {
        throw new ApiException(ex.Message);
      }
    }

    internal static string GetHost() {
      if (AdRequestor.host != null) {
        return AdRequestor.host;
      }

      var host = System.Configuration.ConfigurationManager.AppSettings["ApiHost"];
      if (host != null) {
        return string.Format("http://{0}", host);
      }

      throw new ApiException("You must specify an ApiHost in your application's configuration file.");
    }

    public static void SetHost(string host) {
      AdRequestor.host = host;
    }
  }
}
