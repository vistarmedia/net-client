using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VistarClient.Request;

namespace VistarClient.Entities {
  public class Venue {
    public string Id { get; set; }

    public string NetworkId { get; set; }

    public string Name { get; set; }

    public double CpmFloor { get; set; }

    public string PartnerVenueId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string VenueType { get; set; }

    internal VenueMessage ToMessage() {
      return new VenueMessage {
        id = Id,
        network_id = NetworkId,
        name = Name,
        cpm_floor = CpmFloor,
        partner_venue_id = PartnerVenueId,
        latitude = Latitude,
        longitude = Longitude,
        venue_type = VenueType
      };
    }

    internal static Venue FromMessage(VenueMessage message) {
      return new Venue {
        Id = message.id,
        NetworkId = message.network_id,
        Name = message.name,
        CpmFloor = message.cpm_floor,
        PartnerVenueId = message.partner_venue_id,
        Latitude = message.latitude,
        Longitude = message.longitude,
        VenueType = message.venue_type
      };
    }
  }
}