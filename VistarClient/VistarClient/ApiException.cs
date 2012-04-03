using System;

namespace VistarClient {
	public class ApiException : Exception {
		public ApiException(string message) : base(message) {
		}
	}
}

