using System;
using System.Net;
using VistarClient.Request;
using VistarClient.Utils;

namespace VistarClient.Entities {
  public class Advertisement {
    readonly IVistarWebRequestFactory requestFactory;

    public Advertisement()
      : this(new VistarWebRequestFactory()) {
    }

    public Advertisement(IVistarWebRequestFactory requestFactory) {
      this.requestFactory = requestFactory;
    }

    public string Id { get; private set; }

    public virtual string ProofOfPlayUrl { get; private set; }

    public string ExpirationUrl { get; private set; }

    public long LeaseExpiry { get; private set; }

    public string DisplayAreaId { get; private set; }

    public string AssetId { get; private set; }

    public string AssetUrl { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public string MimeType { get; private set; }

    public int LengthInSeconds { get; private set; }

    public long DisplayTime { get; private set; }

    public DateTime GetDisplayDateTime() {
      return new DateTime().GetLocalFromUtcUnixTime(DisplayTime);
    }

    public void SendProofOfPlay() {
      var request = requestFactory.Create(ProofOfPlayUrl);

      try {
        var response = request.Get();
        response.Close();
      }
      catch (VistarWebException ex) {
        if (ex.StatusCode == HttpStatusCode.BadRequest) {
          throw new InvalidLeaseException();
        }
        else if (ex.StatusCode != HttpStatusCode.OK) {
          throw new ApiException(ex.Message);
        }
      }
    }

    public void SendProofOfPlay(ProofOfPlayMessage message) {
      message.Send(ProofOfPlayUrl);
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
