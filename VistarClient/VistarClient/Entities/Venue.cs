using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VistarClient.Request;

namespace VistarClient.Entities {
  public class Venue {
    public string NetworkId { get; set; }

    public string Name { get; set; }

    public int Gvt { get; set; }

    public int DwellTime { get; set; }

    public double CpmFloor { get; set; }

    public string PartnerVenueId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string StreetAddress { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string ZipCode { get; set; }

    public double NoticePercent { get; set; }

    public bool HasAudio { get; set; }

    public double HoursPerDay { get; set; }

    public int DaysPerWeek { get; set; }

    public string VenueType { get; set; }

    internal VenueMessage ToMessage() {
      return new VenueMessage {
        network_id = NetworkId,
        name = Name,
        raw_gvt = Gvt,
        dwell_time = DwellTime,
        cpm_floor_cents = CpmFloor,
        partner_venue_id = PartnerVenueId,
        latitude = Latitude,
        longitude = Longitude,
        street_address = StreetAddress,
        city = City,
        state = State,
        zip_code = ZipCode,
        notice_percent = NoticePercent,
        has_audio = HasAudio,
        hours_per_day = HoursPerDay,
        days_per_week = DaysPerWeek,
        venue_type = VenueType
      };
    }

    internal static Venue FromMessage(VenueMessage message) {
      return new Venue {
        NetworkId = message.network_id,
        Name = message.name,
        Gvt = message.raw_gvt,
        DwellTime = message.dwell_time,
        CpmFloor = message.cpm_floor_cents,
        PartnerVenueId = message.partner_venue_id,
        Latitude = message.latitude,
        Longitude = message.longitude,
        StreetAddress = message.street_address,
        City = message.city,
        State = message.state,
        ZipCode = message.zip_code,
        NoticePercent = message.notice_percent,
        HasAudio = message.has_audio,
        HoursPerDay = message.hours_per_day,
        DaysPerWeek = message.days_per_week,
        VenueType = message.venue_type
      };
    }
  }
}
