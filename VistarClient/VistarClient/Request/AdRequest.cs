using System;
using System.Collections.Generic;
using VistarClient.Entities;
using Newtonsoft.Json;

namespace VistarClient.Request {
	[JsonObject(MemberSerialization.OptIn)]
	public class AdRequest {
		[JsonProperty(PropertyName="network_id")]
		public string network_id { get; set; }
		
		[JsonProperty(PropertyName="api_key")]
		public string api_key { get; set; }
		
		[JsonProperty(PropertyName="device_id")]
		public string device_id { get; set; }
		
		[JsonProperty(PropertyName="number_of_screens")]
		public int number_of_screens { get; set; }
		
		[JsonProperty(PropertyName="display_time")]
		public long display_time { get; set; }
		
		[JsonProperty(PropertyName="direct_connection")]
		public bool direct_connection { get; set; }
		
		[JsonProperty(PropertyName="display_area")]
		public List<DisplayArea> display_area { get; set; }
		
		[JsonProperty(PropertyName="device_attribute")]
		public List<DeviceAttribute> device_attribute { get; set; }
		
		public DateTime GetDisplayTime() {
			return DateTime.FromFileTimeUtc(display_time);
		}
		
		public void SetDisplayTime(DateTime displayTime) {
			
		}
	}
}