using RestSharp;
using VistarClient.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using VistarClient.Request;
using System.Net;
using System.Text;
using System.IO;

namespace VistarClient {
  public interface IVenueService {
    List<Venue> Get();
    void Create(Venue venue);
    void Update(Venue venue);
  }

  public class VenueService : RestService, IVenueService {

    const string RESOURCE = "/venues/";

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
      var resource = RESOURCE + venue.Id;
      Save(venue, resource, Method.PUT);
    }

    void Save(Venue venue, string resource, Method method) {
      var restRequest = requestFactory.Create(resource, method);
      restRequest.RequestFormat = DataFormat.Json;
      string data = restRequest.JsonSerializer.Serialize(venue.ToMessage());
      restRequest.AddParameter("application/json", data,
        ParameterType.RequestBody);

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
          request.GetResponse();
        }
        catch (WebException ex) {
          throw new ApiException("Error authenticating: " + ex.Message);
        }
      }
    }

    static string GetHost() {
      var host =
        System.Configuration.ConfigurationManager.AppSettings["RestUrl"];

      if (host != null) {
        return host;
      }

      throw new ApiException(
        "You must specify an RestHost in your configuration file.");
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
