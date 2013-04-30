using System;
using System.Net;
using VistarClient.Utils;

namespace VistarClient.Request {
  public class ProofOfPlayMessage {
    readonly IVistarWebRequestFactory requestFactory;

    public DateTime DisplayTime { get; set; }
    public int NumberOfScreens { get; set; }

    public ProofOfPlayMessage()
      : this(new VistarWebRequestFactory()) {
    }

    public ProofOfPlayMessage(IVistarWebRequestFactory requestFactory) {
      this.requestFactory = requestFactory;
    }

    public void Send(string url) {

      var request = requestFactory.Create(url);

      try {
        var response = request.Post(
          string.Format("{{\"display_time\": {0}, \"number_of_screens\": {1}}}",
            DisplayTime.ToUtcUnixTime(), NumberOfScreens));

        response.Close();
      }
      catch (VistarWebException ex) {
        if (ex.StatusCode == HttpStatusCode.BadRequest) {
          throw new InvalidLeaseException();
        }
        else if (ex.StatusCode != HttpStatusCode.OK) {
          throw new ApiException(ex.Message);
        }
      }
    }
  }
}
