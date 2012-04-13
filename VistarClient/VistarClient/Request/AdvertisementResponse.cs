using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VistarClient.Entities;

namespace VistarClient.Request {
  public class AdvertisementResponse {
    [JsonProperty(PropertyName="advertisement")]
    public List<Advertisement> advertisement { get; set; }
  }
}