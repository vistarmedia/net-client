using System;
using System.Collections.Generic;
using VistarClient.Entities;
using Newtonsoft.Json;

namespace VistarClient.Request {
  public class AdvertisementResponse {
    public List<Advertisement> advertisement { get; set; }
  }
}