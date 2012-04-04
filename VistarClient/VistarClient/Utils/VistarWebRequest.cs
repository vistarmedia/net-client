using System;
using System.Net;

namespace VistarClient.Utils { 
  public interface IVistarWebRequest {
    WebResponse GetResponse();
  }
 
  public class VistarWebRequest : IVistarWebRequest {
    readonly HttpWebRequest request;
   
    public VistarWebRequest(HttpWebRequest request) {
      this.request = request;  
    }
   
    public WebResponse GetResponse() {
      try {
        return request.GetResponse();
      }
      catch(WebException ex) {
        throw new VistarWebException(ex, ((HttpWebResponse)ex.Response).StatusCode);
      }
    }
  }
}