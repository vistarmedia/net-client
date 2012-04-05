using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VistarClient.Entities {
  public class DisplayArea {
   [JsonProperty(PropertyName="id")]
    public string Id { get; set; }
   
   [JsonProperty(PropertyName="width")]
    public int Width { get; set; }
   
   [JsonProperty(PropertyName="height")]
    public int Height { get; set; }
   
   [JsonProperty(PropertyName="supported_media")]
    public List<string> SupportedMedia { get; set; }
   
   [JsonProperty(PropertyName="min_duration")]
    public int? MinDuration { get; set; }
   
   [JsonProperty(PropertyName="max_duration")]
    public int? MaxDuration { get; set; }

   [JsonProperty(PropertyName="allow_audio")]
    public bool AllowAudio{ get; set; }
  }
}