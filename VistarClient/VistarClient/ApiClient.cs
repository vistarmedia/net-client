using System;
using VistarClient.Entities;
using VistarClient.Request;
using RestSharp;
using System.Configuration;

namespace VistarClient {
	public class ApiClient {
		const string BASE_URL = @"http://dev.api.vistarmedia.com/api/v1/";
		
		public Advertisement GetAd(AdRequest request){
			
			var client = new RestClient(BASE_URL);
			var restRequest = new RestRequest("get_ad/json", Method.POST);
			
			restRequest.RequestFormat = DataFormat.Json;
			restRequest.AddParameter("text/json", restRequest.JsonSerializer.Serialize(request), ParameterType.RequestBody);
	
			return client.Execute<AdvertisementResponse>(restRequest).Data.advertisement[0];
		}
	}
}

