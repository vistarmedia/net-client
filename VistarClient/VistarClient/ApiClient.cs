using System;
using System.Linq;
using RestSharp;
using VistarClient.Entities;
using VistarClient.Request;

namespace VistarClient {
  public class ApiClient {
   
    readonly IRestClient restClient;
    readonly IRestRequest restRequest;
       
    public ApiClient() : this(new RestClient(GetHost()), new RestRequest("api/v1/get_ad/json", Method.POST)) {
     
    }
   
    public ApiClient(IRestClient restClient, IRestRequest restRequest) {
      this.restClient = restClient;
      this.restRequest = restRequest;
    }
       
    public Advertisement SubmitAdRequest(AdRequest request) {
      restRequest.RequestFormat = DataFormat.Json;
      string data = restRequest.JsonSerializer.Serialize(request);
     
      restRequest.AddParameter("text/json", data, ParameterType.RequestBody);
     
      try {
        return restClient.Execute<AdvertisementResponse>(restRequest).Data.Advertisements.First();
      }
      catch(Exception ex) {
        throw new ApiException(ex.Message);  
      }
    }
   
    static string GetHost() {
      var host = System.Configuration.ConfigurationManager.AppSettings["ApiHost"];
      if(host != null) {
        return string.Format("http://{0}", host);
      }
     
      throw new ApiException("You must specify an ApiHost in your application's configuration file.");
    }
  }
}