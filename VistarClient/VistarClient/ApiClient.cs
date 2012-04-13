using System;
using System.Linq;
using RestSharp;
using VistarClient.Entities;
using VistarClient.Request;
using System.Threading;
using System.Collections.Generic;

namespace VistarClient {
  public class ApiClient {
    readonly IAdRequestor adRequestor;

    public ApiClient() : this(new AdRequestor(new RestClient(AdRequestor.GetHost()), new RestRequest(AdRequestor.RESOURCE, AdRequestor.METHOD))) {

    }

    public ApiClient(IAdRequestor adRequestor) {
      this.adRequestor = adRequestor;
    }

    public Advertisement SubmitAdRequest(AdRequest request) {
      var ads = adRequestor.RunSubmitAdRequest(request);
      return ads.First();
    }
  }
