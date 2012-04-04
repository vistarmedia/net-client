using System;
using System.Net;
using VistarClient.Utils;

namespace VistarClient.Entities {
  public class Advertisement {
    readonly IVistarWebRequestFactory requestFactory;
		
    public Advertisement() : this(new VistarWebRequestFactory()) {
    }
		
    public Advertisement(IVistarWebRequestFactory requestFactory) {
      this.requestFactory = requestFactory;
    }
		
    public string id { get; set; }

    public string proof_of_play_url { get; set; }

    public long lease_expiry { get; set; }

    public string display_area_Id { get; set; }

    public string asset_id { get; set; }
		
    public string asset_url { get; set; }
		
    public int width { get; set; }

    public int height { get; set; }

    public string mime_type { get; set; }

    public int length_in_seconds{ get; set; }
		
    public void SendProofOfPlay() {
      var request = requestFactory.Create(proof_of_play_url);
			
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

