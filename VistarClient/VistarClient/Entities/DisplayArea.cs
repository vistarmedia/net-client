using System;
using System.Collections.Generic;
using VistarClient.Request;

namespace VistarClient.Entities {
  public class DisplayArea {
    public string Id { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public List<string> SupportedMedia { get; set; }

    public int? MinDuration { get; set; }

    public int? MaxDuration { get; set; }

    public bool AllowAudio { get; set; }

    public int CpmFloorCents { get; set; }

    internal DisplayAreaMessage ToMessage() {
      return new DisplayAreaMessage {
        id = Id,
        width = Width,
        height = Height,
        supported_media = SupportedMedia,
        min_duration = MinDuration,
        max_duration = MaxDuration,
        allow_audio = AllowAudio,
        cpm_floor_cents = CpmFloorCents
      };
    }
  }
}
