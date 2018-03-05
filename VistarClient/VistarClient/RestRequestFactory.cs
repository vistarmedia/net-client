using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace VistarClient {
  public interface IRestRequestFactory {
    IRestRequest Create(string resource, Method method);
  }

  public sealed class RestRequestFactory : IRestRequestFactory {
    public IRestRequest Create(string resource, Method method) {
      return new RestRequest(resource, method);
    }
  }
}
