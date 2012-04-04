using System;
using System.Net;

namespace VistarClient.Utils {
  public interface IVistarWebRequestFactory {
    IVistarWebRequest Create(string url);
  }
	
  public class VistarWebRequestFactory : IVistarWebRequestFactory {
    public IVistarWebRequest Create(string url) {
      return new VistarWebRequest((HttpWebRequest)WebRequest.Create(url));
    }
  }
}