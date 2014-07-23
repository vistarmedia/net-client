using RestSharp;
using VistarClient.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using VistarClient.Request;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace VistarClient {
  public interface IVenueService {
    List<Venue> Get();
    void Create(Venue venue);
    void Update(Venue venue);
  }

  public class VenueService : RestService, IVenueService {

    class VenueRequest {
      public Venue Venue { get; private set; }
      public string Resource { get; private set; }
      public Method Method { get; private set; }

      public VenueRequest(Venue venue, string saveResource, Method method) {
        if(venue.HasPendingEdits && method != Method.POST) {
          throw new ApiException(
            "Cannot edit venue pending approval: " + venue.PartnerVenueId);
        }
        Venue = venue;
        Resource = saveResource;
        Method = method;
      }
    }

    public class VenueResponse {
      public string PartnerVenueId { get; private set; }
      public HttpStatusCode ResponseCode { get; private set; }
      public string ResponseText { get; private set; }

      public VenueResponse(string partnerVenueId,
          HttpStatusCode responseCode, string responseText) {
        PartnerVenueId = partnerVenueId;
        ResponseCode = responseCode;
        ResponseText = responseText;
      }
    }

    const string RESOURCE = "/selling/venues/";

    public VenueService()
      : base(new RestClient(GetHost()), new RestRequestFactory()) {

      restClient.CookieContainer = new CookieContainer();
      Authenticate();
    }

    public List<Venue> Get() {
      var restRequest = requestFactory.Create(RESOURCE, Method.GET);
      restRequest.OnBeforeDeserialization = resp => {
        resp.ContentType = "application/json";
      };
      var venues = restClient.Execute<List<VenueMessage>>(restRequest);
      return venues.Data.Select(v => Venue.FromMessage(v)).ToList();
    }

    public void Create(Venue venue) {
      Save(venue, RESOURCE, Method.POST);
    }

    public void Update(Venue venue) {
      if(venue.HasPendingEdits) {
        throw new ApiException(
          "Cannot edit venue pending approval: " + venue.PartnerVenueId);
      }
      var updateResource = RESOURCE + venue.Id;
      Save(venue, updateResource, Method.PUT);
    }

    void Save(Venue venue, string saveResource, Method method) {
      var restRequest = GetRestRequest(venue, saveResource, method);

      try {
        var response = restClient.Execute(restRequest);
        if (response.StatusCode != HttpStatusCode.Created
                  && response.StatusCode != HttpStatusCode.OK) {
          throw new ApiException("Error saving venue: " + response.Content);
        }
      }
      catch (Exception ex) {
        throw new ApiException(ex.Message);
      }
    }

    public List<Task<VenueResponse>> Create(IEnumerable<Venue> venues) {
      var requests = venues.Select(v =>
        new VenueRequest(v, RESOURCE, Method.POST));
      return Execute(requests);
    }

    public List<Task<VenueResponse>> Update(IEnumerable<Venue> venues) {
      return Execute(venues.Select(v =>
        new VenueRequest(v, RESOURCE + v.Id, Method.PUT)));
    }

    public List<Task<VenueResponse>> Delete(IEnumerable<Venue> venues) {
      return Execute(venues.Select(v =>
        new VenueRequest(v, RESOURCE + v.Id, Method.DELETE)));
    }

    List<Task<VenueResponse>> Execute(
          IEnumerable<VenueRequest> requests) {
      var tasks = new List<Task<VenueResponse>>();
      foreach (var request in requests) {
        var req = request;
        var task = Task.Factory.StartNew(() => {
          return DoAction(req);
        });
        tasks.Add(task);
      }
      return tasks;
    }

    VenueResponse DoAction(VenueRequest request) {
      var restRequest = GetRestRequest(request.Venue, request.Resource,
        request.Method);

      try {
        var response = restClient.Execute(restRequest);
        return new VenueResponse(request.Venue.PartnerVenueId,
          response.StatusCode, response.StatusDescription);
      }
      catch (Exception ex) {
        return new VenueResponse(request.Venue.PartnerVenueId,
          HttpStatusCode.InternalServerError, ex.Message);
      }
    }

    void Authenticate() {
      var request = (HttpWebRequest)WebRequest.Create(GetHost() + "/session/");
      request.CookieContainer = restClient.CookieContainer;

      request.Method = "POST";
      request.ContentType = "application/json";

      var data = string.Format(
        "{{\"email\": \"{0}\", \"password\": \"{1}\"}}", GetUser(),
        GetPassword());

      using (var writer = new StreamWriter(request.GetRequestStream())) {
        writer.Write(data);
        writer.Flush();

        try {
          var response = request.GetResponse();
          response.Close();
        }
        catch (WebException ex) {
          throw new ApiException("Error authenticating: " + ex.Message);
        }
      }
    }

    IRestRequest GetRestRequest(Venue venue, string getResource, Method method) {
      var restRequest = requestFactory.Create(getResource, method);
      restRequest.RequestFormat = DataFormat.Json;
      string data = restRequest.JsonSerializer.Serialize(venue.ToMessage());
      data = Encoding.ASCII.GetString(Encoding.UTF8.GetBytes(data));

      restRequest.AddParameter("application/json", data,
        ParameterType.RequestBody);
      restRequest.Timeout = 500;
      return restRequest;
    }

    static string GetHost() {
      var host =
        System.Configuration.ConfigurationManager.AppSettings["RestUrl"];

      if (host != null) {
        return host;
      }

      throw new ApiException(
        "You must specify an RestUrl in your configuration file.");
    }

    static string GetUser() {
      var user =
        System.Configuration.ConfigurationManager.AppSettings["RestUser"];

      if (user != null) {
        return user;
      }

      throw new ApiException(
        "You must specify an RestUser in your configuration file.");
    }

    static string GetPassword() {
      var password =
        System.Configuration.ConfigurationManager.AppSettings["RestPass"];

      if (password != null) {
        return password;
      }

      throw new ApiException(
        "You must specify an RestPass in your configuration file.");
    }
  }
}
