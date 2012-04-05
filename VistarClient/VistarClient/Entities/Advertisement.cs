using System.Net;
using Newtonsoft.Json;
using VistarClient.Utils;

namespace VistarClient.Entities {
  public class Advertisement {
    readonly IVistarWebRequestFactory requestFactory;
		
    public Advertisement() : this(new VistarWebRequestFactory()) {
    }
		
    public Advertisement(IVistarWebRequestFactory requestFactory) {
      this.requestFactory = requestFactory;
    }

    [JsonProperty(PropertyName="id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName="proof_of_play_url")]
    public string ProofOfPlayUrl { get; set; }

    [JsonProperty(PropertyName="lease_expiry")]
    public long LeaseExpiry { get; set; }

    [JsonProperty(PropertyName="display_area_id")]
    public string DisplayAreaId { get; set; }

    [JsonProperty(PropertyName="asset_id")]
    public string AssetId { get; set; }

    [JsonProperty(PropertyName="asset_url")]
    public string AssetUrl { get; set; }

    [JsonProperty(PropertyName="width")]
    public int Width { get; set; }

    [JsonProperty(PropertyName="height")]
    public int Height { get; set; }

    [JsonProperty(PropertyName="mime_type")]
    public string MimeType { get; set; }

    [JsonProperty(PropertyName="length_in_seconds")]
    public int LengthInSeconds{ get; set; }
		
    public void SendProofOfPlay() {
      var request = requestFactory.Create(ProofOfPlayUrl);
			
      try {
        request.GetResponse();
      }
      catch(VistarWebException ex) {
        if(ex.StatusCode == HttpStatusCode.BadRequest) {
          throw new InvalidLeaseException();	
        }
        else if(ex.StatusCode != HttpStatusCode.NoContent) {
          throw new ApiException(ex.Message);
        }
      }
    }
  }
}

