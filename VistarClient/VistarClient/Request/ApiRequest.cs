using System;

namespace VistarClient.Request {
	public abstract class ApiRequest {
		public string NetworkId{ get; set; }

		public string ApiKey{ get; set; }

		public string DeviceId{ get; set; }

		public int NumberOfScreens{ get; set; }

		public long DisplayTime{ get; set; }

		public bool DirectConnection{ get; set; }
	}
}