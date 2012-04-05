using System;
using Newtonsoft.Json;

namespace VistarClient.Entities {
  public class DeviceAttribute {
    [JsonProperty(PropertyName="name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName="value")]
    public string Value { get; set; }
  }
}

