using System;
using VistarClient.Entities;
using VistarClient.Request;
using RestSharp;

namespace VistarClient {
	public class ApiClient {
		static string host = GetHost();
		
		public Advertisement GetAd(AdRequest request) {
			var client = new RestClient(host);
			var restRequest = new RestRequest("api/v1/get_ad/json", Method.POST);
			
			restRequest.RequestFormat = DataFormat.Json;
			string data = restRequest.JsonSerializer.Serialize(request);
			restRequest.AddParameter("text/json", data, ParameterType.RequestBody);
			
			return client.Execute<AdvertisementResponse>(restRequest).Data.advertisement[0];
		}
		
		static string GetHost() {
			var host = System.Configuration.ConfigurationManager.AppSettings["ApiHost"];
			if(host != null) {
				return string.Format("http://{0}", host);
			}
			
			throw new ApiException("You must specify an ApiHost in your application's configuration file.");
		}
	}
}

