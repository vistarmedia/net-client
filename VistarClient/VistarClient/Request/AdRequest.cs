using System;
using System.Collections.Generic;
using RestSharp.Serializers;
using VistarClient.Entities;

namespace VistarClient.Request {
	public class AdRequest {
//		[SerializeAs(Name="network_id")]
//		public string NetworkId{ get; set; }
//
//		[SerializeAs(Name="api_key")]
//		public string ApiKey{ get; set; }
//
//		[SerializeAs(Name="device_id")]
//		public string DeviceId{ get; set; }
//
//		[SerializeAs(Name="number_of_screens")]
//		public int NumberOfScreens{ get; set; }
//
//		[SerializeAs(Name="display_time")]
//		public long DisplayTime{ get; set; }
//
//		[SerializeAs(Name="direct_connection")]
//		public bool DirectConnection{ get; set; }
		
		public string network_id { get; set; }

		public string api_key { get; set; }
		
		public string device_id { get; set; }

		public int number_of_screens { get; set; }

		public long display_time { get; set; }

		public bool direct_connection { get; set; }
		
		public List<DisplayArea> display_area { get; set; }

		public List<DeviceAttribute> device_attribute { get; set; }
	}
}