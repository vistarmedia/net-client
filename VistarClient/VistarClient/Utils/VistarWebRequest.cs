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
          Console.WriteLine("URL: {0}\n", request.RequestUri);
        }
        return request.GetResponse();
      }
      catch (WebException ex) {        
        if (ex.Response == null) {          
          throw new ApiException(ex.Message);
        }
        throw new VistarWebException(ex, ((HttpWebResponse)ex.Response).StatusCode);
      }
    }
  }
}