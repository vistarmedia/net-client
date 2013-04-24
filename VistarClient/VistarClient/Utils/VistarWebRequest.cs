using System;
using System.Net;
using System.IO;
using System.Text;

namespace VistarClient.Utils {
  public interface IVistarWebRequest {
    WebResponse Get();
    WebResponse Post(string postData);
  }

  public class VistarWebRequest : IVistarWebRequest {
    readonly HttpWebRequest request;

    internal VistarWebRequest(HttpWebRequest request) {
      this.request = request;
    }

    public WebResponse Get() {
      try {
        if (VistarGlobals.IsDebug) {
          Console.WriteLine("INITIATING GET REQUEST...\n");
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

    public WebResponse Post(string postData) {
      try {
        if (VistarGlobals.IsDebug) {
          Console.WriteLine("INITIATING POST REQUEST...\n");
          Console.WriteLine("URL: {0}\n", request.RequestUri);
        }
        request.Method = "POST";
        request.ContentType = "text/json";
        request.ContentLength = postData.Length;

        using (var writer = new StreamWriter(request.GetRequestStream(),
            Encoding.ASCII)) {
          writer.Write(postData);
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