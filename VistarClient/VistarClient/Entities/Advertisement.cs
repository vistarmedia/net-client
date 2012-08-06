using System.Net;
using VistarClient.Utils;
using VistarClient.Request;
using System;

namespace VistarClient.Entities {
  public class Advertisement {
    readonly IVistarWebRequestFactory requestFactory;

    public Advertisement()
      : this(new VistarWebRequestFactory()) {
    }

    public Advertisement(IVistarWebRequestFactory requestFactory) {
      this.requestFactory = requestFactory;
    }

    public string Id { get; set; }

    public string ProofOfPlayUrl { get; set; }

    public string ExpirationUrl { get; set; }

    public long LeaseExpiry { get; set; }

    public string DisplayAreaId { get; set; }

    public string AssetId { get; set; }

    public string AssetUrl { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public string MimeType { get; set; }

    public int LengthInSeconds { get; set; }

    public long DisplayTime { get; set; }

    public DateTime GetDisplayDateTime() {
      return new DateTime().GetLocalFromUtcUnixTime(DisplayTime);
    }

    public void SetDisplayDateTime(DateTime displayTime) {
      DisplayTime = displayTime.ToUtcUnixTime();
    }

    public void SendProofOfPlay() {
      var request = requestFactory.Create(ProofOfPlayUrl);

      try {
        request.GetResponse();
      }
      catch (VistarWebException ex) {
        if (ex.StatusCode == HttpStatusCode.BadRequest) {
          throw new InvalidLeaseException();
        }
        else if (ex.StatusCode != HttpStatusCode.NoContent) {
          throw new ApiException(ex.Message);
        }
      }
    }

    internal static Advertisement FromMessage(AdvertisementMessage message) {
      return new Advertisement {
        Id = message.id,
        ProofOfPlayUrl = message.proof_of_play_url,
        ExpirationUrl = message.expiration_url,
        LeaseExpiry = message.lease_expiry,
        DisplayAreaId = message.display_area_id,
        AssetId = message.asset_id,
        AssetUrl = message.asset_url,
        Width = message.width,
        Height = message.height,
        MimeType = message.mime_type,
        LengthInSeconds = message.length_in_seconds,
        DisplayTime = message.display_time,
      };
    }
  }
}

