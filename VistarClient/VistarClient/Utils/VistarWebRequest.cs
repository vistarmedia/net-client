using System;
using System.Net;

namespace VistarClient.Utils {
  public interface IVistarWebRequest {
    WebResponse GetResponse();
  }

  public class VistarWebRequest : IVistarWebRequest {
    readonly HttpWebRequest request;

    internal VistarWebRequest(HttpWebRequest request) {
      this.request = request;
    }

    public WebResponse GetResponse() {
      try {
        if (VistarGlobals.IsDebug) {
          Console.WriteLine("INITIATING WEB REQUEST...\n");
          Console.WriteLine("Request is null? {0}\n", request == null);
          Console.WriteLine("URL: {0}\n", request.RequestUri);
        }
        return request.GetResponse();
      }
      catch (WebException ex) {
        if (VistarGlobals.IsDebug) {
          Console.WriteLine("WebException: {0}, StackTrace: {1}",
            ex.Message, ex.StackTrace);
        }
        throw new VistarWebException(ex, ((HttpWebResponse)ex.Response).StatusCode);
      }
    }
  }
}