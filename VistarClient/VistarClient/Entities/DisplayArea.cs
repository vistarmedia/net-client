using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VistarClient.Entities {
 [JsonObject(MemberSerialization.OptIn)]
  public class DisplayArea {
   [JsonProperty(PropertyName="id")]
    public string id { get; set; }
   
   [JsonProperty(PropertyName="width")]
    public int width { get; set; }
   
   [JsonProperty(PropertyName="height")]
    public int height { get; set; }
   
   [JsonProperty(PropertyName="supported_media")]
    public List<string> supported_media { get; set; }
   
   [JsonProperty(PropertyName="min_duration")]
    public int? min_duration { get; set; }
   
   [JsonProperty(PropertyName="max_duration")]
    public int? max_duration { get; set; }

   [JsonProperty(PropertyName="allow_audio")]
    public bool allow_audio{ get; set; }
  }
}