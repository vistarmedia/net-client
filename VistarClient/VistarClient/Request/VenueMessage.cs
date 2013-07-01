using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VistarClient.Request {
  public class VenueMessage {
    public string id { get; set; }

    public string network_id { get; set; }

    public string name { get; set; }

    public double cpm_floor { get; set; }

    public string partner_venue_id { get; set; }

    public double latitude { get; set; }

    public double longitude { get; set; }

    public string venue_type { get; set; }
  }
}
