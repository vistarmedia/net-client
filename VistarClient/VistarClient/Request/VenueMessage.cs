using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VistarClient.Request {
  public class VenueMessage {
    public string id { get; set; }

    public string network_id { get; set; }

    public string name { get; set; }

    public long gvt { get; set; }

    public int gvt_seconds { get; set; }

    public int dwell_time { get; set; }

    public double? impressions_per_spot { get; set; }

    public double? impressions_per_sec { get; set; }

    public double cpm_floor_cents { get; set; }

    public string partner_venue_id { get; set; }

    public double latitude { get; set; }

    public double longitude { get; set; }

    public string street_address { get; set; }

    public string city { get; set; }

    public string state { get; set; }

    public string zip_code { get; set; }

    public string venue_type { get; set; }

    public bool has_pending_edits { get; set; }
  }
}
