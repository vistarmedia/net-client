using System;
using System.Net;

namespace VistarClient.Utils {
  public class VistarWebException : Exception {
    public HttpStatusCode StatusCode { get; private set; }

    public VistarWebException(WebException innerException, HttpStatusCode statusCode)
      : base(innerException.Message, innerException) {
      StatusCode = statusCode;
    }
  }
}
