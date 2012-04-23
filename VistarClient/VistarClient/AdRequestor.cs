using System;
using System.Collections.Generic;
using RestSharp;
using VistarClient.Entities;
using VistarClient.Request;

namespace VistarClient {
  public interface IAdRequestor {
    List<Advertisement> RunSubmitAdRequest(AdRequest request);
  }

  public sealed class AdRequestor : IAdRequestor {
    readonly IRestClient restClient;
    readonly IRestRequest restRequest;

    internal const string RESOURCE = "api/v1/get_ad/json";
    internal const Method METHOD = Method.POST;

    public AdRequestor() : this(new RestClient(GetHost()), new RestRequest(RESOURCE, METHOD)) {

    }

    public AdRequestor(IRestClient restClient, IRestRequest restRequest) {
      this.restClient = restClient;
      this.restRequest = restRequest;
    }

    public List<Advertisement> RunSubmitAdRequest(AdRequest request) {
      restRequest.RequestFormat = DataFormat.Json;
      string data = restRequest.JsonSerializer.Serialize(request);
      restRequest.AddParameter("text/json", data, ParameterType.RequestBody);

      try {
        Console.WriteLine("Sending request... {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
        var response = restClient.Execute<AdvertisementResponse>(restRequest);
        Console.WriteLine("Recieved response... {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
        var ads = response.Data.advertisement;
        return ads;
      }
      catch(Exception ex) {
        throw new ApiException(ex.Message);
      }
    }
   
    internal static string GetHost() {
      var host = System.Configuration.ConfigurationManager.AppSettings["ApiHost"];
      if(host != null) {
        return string.Format("http://{0}", host);
      }
     
      throw new ApiException("You must specify an ApiHost in your application's configuration file.");
    }
  }
}
