using System;
using System.Collections.Generic;
using RestSharp.Serializers;

namespace VistarClient.Entities {
//	[SerializeAs(Name = "display_area")]
	public class DisplayArea {
//		[SerializeAs(Name="id")]
//		public string Id{ get; set; }
//		
//		[SerializeAs(Name="width")]
//		public int Width{ get; set; }
//
//		[SerializeAs(Name="height")]
//		public int Height{ get; set; }
//
//		[SerializeAs(Name="supported_media")]
//		public List<string> SupportedMedia{ get; set; }
//
//		[SerializeAs(Name="min_duration")]
//		public int? MinDuration { get; set; }
//
//		[SerializeAs(Name="max_duration")]
//		public int? MaxDuration{ get; set; }
//
//		[SerializeAs(Name="allow_audio")]
//		public bool AllowAudio{ get; set; }
		
		public string id { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public List<string> supported_media { get; set; }
		public int? min_duration { get; set; }
		public int? max_duration { get; set; }
		public bool allow_audio{ get; set; }
	}
}

