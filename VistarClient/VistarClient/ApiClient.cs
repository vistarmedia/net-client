using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RestSharp;
using VistarClient.Entities;
using VistarClient.Request;

namespace VistarClient {
  public class ApiClient {
    readonly IAdRequestor adRequestor;

    public ApiClient()
      : this(new AdRequestor()) {

    }

    public ApiClient(IAdRequestor adRequestor) {
      this.adRequestor = adRequestor;
    }

    public List<Advertisement> SubmitAdRequest(AdRequest request) {
      return adRequestor.RunSubmitAdRequest(request);
    }
  }
}
