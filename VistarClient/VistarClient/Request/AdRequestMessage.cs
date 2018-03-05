using System;
using System.Collections.Generic;
using VistarClient.Entities;
using VistarClient.Utils;

namespace VistarClient.Request {
  public class AdRequestMessage {
    public string network_id { get; set; }

    public string api_key { get; set; }

    public string device_id { get; set; }

    public long display_time { get; set; }

    public bool direct_connection { get; set; }

    public decimal? latitude { get; set; }

    public decimal? longitude { get; set; }

    public string dma { get; set; }

    public string venue_id { get; set; }

    public int? duration { get; set; }

    public int? interval { get; set; }

    public List<DisplayAreaMessage> display_area { get; set; }

    public List<DeviceAttributeMessage> device_attribute { get; set; }
  }
}
