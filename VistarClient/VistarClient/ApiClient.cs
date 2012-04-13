using System;
using System.Linq;
using RestSharp;
using VistarClient.Entities;
using VistarClient.Request;
using System.Threading;
using System.Collections.Generic;

namespace VistarClient {
  public class ApiClient {
    public static List<Advertisement> advertisements;

    readonly IRestClient restClient;
    readonly IRestRequest restRequest;
       
    public ApiClient() : this(new RestClient(GetHost()), new RestRequest("api/v1/get_ad/json", Method.POST)) {
     
    }
   
    public ApiClient(IRestClient restClient, IRestRequest restRequest) {
      this.restClient = restClient;
      this.restRequest = restRequest;
    }

    public List<Advertisement> SubmitAdRequests(List<AdRequest> requests) {
      var events = new ManualResetEvent[requests.Count];

      RequestState state;

      advertisements = new List<Advertisement>();

      var i = 0;
      foreach(var request in requests) {
        events[i] = new ManualResetEvent(false);
        state = new RequestState(request, restClient, restRequest);
        ThreadPool.QueueUserWorkItem(new WaitCallback(Requestor.MakeRequest), state);
        i++;
      }

      if(WaitHandle.WaitAll(events, 10000, false)) {
        return advertisements;
      }

      throw new Exception("oops");
    }

    static string GetHost() {
      var host = System.Configuration.ConfigurationManager.AppSettings["ApiHost"];
      if(host != null) {
        return string.Format("http://{0}", host);
      }
     
      throw new ApiException("You must specify an ApiHost in your application's configuration file.");
    }
  }

  class Requestor {
    public static void MakeRequest(object state) {
      var requestState = (RequestState)state;
      requestState.RestRequest.RequestFormat = DataFormat.Json;
      string data = requestState.RestRequest.JsonSerializer.Serialize(requestState.AdRequest);

      requestState.RestRequest.AddParameter("text/json", data, ParameterType.RequestBody);

      try {
        var ad = requestState.RestClient.Execute<AdvertisementResponse>(requestState.RestRequest).Data.Advertisements.First();
        ApiClient.advertisements.Add(ad);
      }
      catch(Exception ex) {
        throw new ApiException(ex.Message);
      }
    }
  }

  class RequestState {
    AdRequest adRequest;
    IRestClient restClient;
    IRestRequest restRequest;

    public RequestState(AdRequest adRequest, IRestClient restClient, IRestRequest restRequest) {
      this.adRequest = adRequest;
      this.restClient = restClient;
      this.restRequest = restRequest;
    }

    public AdRequest AdRequest {
      get{ return adRequest;}
      set{ this.adRequest = value;}
    }

    public IRestClient RestClient {
      get{ return restClient;}
      set{ this.restClient = value;}
    }

    public IRestRequest RestRequest {
      get{ return restRequest;}
      set{ this.restRequest = value;}
    }
  }
}