using System;
using System.Collections.Generic;
using System.Linq;
using VistarClient.Entities;
using VistarClient.Utils;
using VistarClient.Request;

namespace VistarClient.Entities {
  public class AdRequest {
    public string NetworkId { get; set; }

    public string ApiKey { get; set; }

    public string DeviceId { get; set; }

    public int NumberOfScreens { get; set; }

    public long DisplayTime { get; set; }

    public bool DirectConnection { get; set; }

    public List<DisplayArea> DisplayAreas { get; set; }

    public List<DeviceAttribute> DeviceAttributes { get; set; }

    public DateTime GetDisplayDateTime() {
      return new DateTime().GetLocalFromUtcUnixTime(DisplayTime);
    }

    public void SetDisplayDateTime(DateTime displayTime) {
      DisplayTime = displayTime.ToUtcUnixTime();
    }

    internal AdRequestMessage ToMessage() {
      return new AdRequestMessage {
        network_id = NetworkId,
        api_key = ApiKey,
        device_id = DeviceId,
        number_of_screens = NumberOfScreens,
        display_time = DisplayTime,
        direct_connection = DirectConnection,
        display_area = DisplayAreas.Select(da => da.ToMessage()).ToList(),
        device_attribute = DeviceAttributes.Select(dattr => dattr.ToMessage()).ToList()
      };
    }
  }
}