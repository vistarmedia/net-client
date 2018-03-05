using System;

namespace VistarClient {
  public class InvalidLeaseException : ApiException {
    public InvalidLeaseException()
      : base("An error occurred transmitting the proof of play: Invalid lease.") {

    }
  }
}
