using System;
using System.Collections.Generic;

namespace VistarClient.Entities {
  public class DisplayArea {
    public string id { get; set; }
   
    public int width { get; set; }
   
    public int height { get; set; }
   
    public List<string> supported_media { get; set; }
   
    public int? min_duration { get; set; }
   
    public int? max_duration { get; set; }

    public bool allow_audio{ get; set; }
  }
}