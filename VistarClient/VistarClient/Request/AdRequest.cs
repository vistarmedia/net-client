using System;
using System.Collections.Generic;
using VistarClient.Entities;
using VistarClient.Utils;

namespace VistarClient.Request {
  public class AdRequest {
    public string network_id { get; set; }
   
    public string api_key { get; set; }
   
    public string device_id { get; set; }

    public int number_of_screens { get; set; }
   
    public long display_time { get; set; }
   
    public bool direct_connection { get; set; }
   
    public List<DisplayArea> display_area { get; set; }

    public List<DeviceAttribute> device_attribute { get; set; }

    public DateTime GetDisplayDateTime() {
      return new DateTime().GetLocalFromUtcUnixTime(display_time);
    }

    public void SetDisplayDateTime(DateTime displayTime) {
      display_time = displayTime.ToUtcUnixTime();
    }
  }
}