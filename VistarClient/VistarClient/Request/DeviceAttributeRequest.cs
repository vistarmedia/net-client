using System;
using System.Collections.Generic;
using VistarClient.Entities;

namespace VistarClient.Request {
	public class DeviceAttributeRequest : ApiRequest {
		public List<DeviceAttribute> device_attribute { get; set; }
	}
}
