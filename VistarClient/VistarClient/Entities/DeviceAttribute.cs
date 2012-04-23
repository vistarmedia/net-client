using System;
using VistarClient.Request;

namespace VistarClient.Entities {
  public class DeviceAttribute {
    public string Name { get; set; }

    public string Value { get; set; }

    internal DeviceAttributeMessage ToMessage() {
      return new DeviceAttributeMessage {
        name = Name,
        value = Value
      };
    }
  }
}

