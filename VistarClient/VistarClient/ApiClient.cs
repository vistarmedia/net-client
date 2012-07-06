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

    public ApiClient()
      : this(new AdRequestor(new RestClient(AdRequestor.GetHost()), new RestRequestFactory())) {

    }

    public ApiClient(IAdRequestor adRequestor) {
      this.adRequestor = adRequestor;
    }

    public List<Advertisement> SubmitAdRequest(AdRequest request) {
      return adRequestor.RunSubmitAdRequest(request);
    }
  }
}