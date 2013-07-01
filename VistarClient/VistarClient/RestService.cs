using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace VistarClient {
  public abstract class RestService {
    protected readonly IRestClient restClient;
    protected readonly IRestRequestFactory requestFactory;

    protected const Method METHOD = Method.POST;

    public RestService(IRestClient restClient,
        IRestRequestFactory requestFactory) {
      this.restClient = restClient;
      this.requestFactory = requestFactory;
    }
  }
}