using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VistarClient.Entities;

namespace VistarClient.Request {
  public class AdRequest {
   [JsonProperty(PropertyName="network_id")]
    public string NetworkId { get; set; }
   
   [JsonProperty(PropertyName="api_key")]
    public string ApiKey { get; set; }
   
   [JsonProperty(PropertyName="device_id")]
    public string DeviceId { get; set; }
   
   [JsonProperty(PropertyName="number_of_screens")]
    public int NumberOfScreens { get; set; }
   
   [JsonProperty(PropertyName="display_time")]
    public long DisplayTime { get; set; }
   
   [JsonProperty(PropertyName="direct_connection")]
    public bool DirectConnection { get; set; }
   
   [JsonProperty(PropertyName="display_area")]
    public List<DisplayArea> DisplayAreas { get; set; }

   [JsonProperty(PropertyName="device_attribute")]
    public List<DeviceAttribute> DeviceAttributes { get; set; }

    public DateTime GetDisplayDateTime() {
      return DateTime.FromFileTimeUtc(DisplayTime);
    }

    public void SetDisplayDateTime(DateTime displayTime) {
      DisplayTime = displayTime.ToFileTimeUtc();
    }
  }
}