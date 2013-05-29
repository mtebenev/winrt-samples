using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mt.Common.AppCore.RestHttpClient
{
	/// <summary>
	/// Basic implementation of web service client
	/// </summary>
	public abstract class WsHttpClientBase : IWsHttpClient
	{
		public async Task<Stream> GetStreamAsync(IWsRequest request)
		{
			HttpResponseMessage responseMessage;
			using(HttpClient client = new HttpClient())
			{
				HttpRequestMessage requestMessage = PrepareRequestMessage(request, HttpMethod.Get);
				responseMessage = await client.SendAsync(requestMessage);
			}

			return await responseMessage.Content.ReadAsStreamAsync();
		}

		public Task<string> GetStringAsync(IWsRequest request)
		{
			HttpClient client = new HttpClient();
			string url =
	"http://open.api.sandbox.ebay.com/shopping?callname=GetSingleItem&appid=MaximTeb-0c83-4085-84a6-d2a968d5c484&version=813&ItemID=110114549046&responseencoding=XML";

			return client.GetStringAsync(url);
		}

		public async Task<string> PostStringAsync(IWsRequest request)
		{
			HttpResponseMessage responseMessage;
			using(HttpClient client = new HttpClient())
			{
				HttpRequestMessage requestMessage = PrepareRequestMessage(request, HttpMethod.Post);
				responseMessage = await client.SendAsync(requestMessage);
			}

			return await responseMessage.Content.ReadAsStringAsync();
		}

		public async Task<Stream> PostStreamAsync(IWsRequest request)
		{
			HttpResponseMessage responseMessage;
			using(HttpClient client = new HttpClient())
			{
				HttpRequestMessage requestMessage = PrepareRequestMessage(request, HttpMethod.Post);
				responseMessage = await client.SendAsync(requestMessage);
			}

			return await responseMessage.Content.ReadAsStreamAsync();
		}


		// Overridables
		protected abstract string BaseUrl { get; }

		/// <summary>
		/// Prepares message for request
		/// </summary>
		private HttpRequestMessage PrepareRequestMessage(IWsRequest request, HttpMethod httpMethod)
		{
			StringBuilder sb = new StringBuilder(this.BaseUrl);

			// Prepare query string parameters
			bool isFirst = true;
			foreach(KeyValuePair<string, string> parameter in request.Parameters)
			{
				if(!isFirst)
					sb.Append("&");
				else
				{
					sb.Append("?");
					isFirst = false;
				}

				string formattedParameter = String.Format("{0}={1}", 
					WebUtility.UrlEncode(parameter.Key),
					WebUtility.UrlEncode(parameter.Value));

				sb.Append(formattedParameter);
			}

			// Prepare HTTP headers
			HttpRequestMessage httpRequest = new HttpRequestMessage(httpMethod, sb.ToString());

			foreach(KeyValuePair<string, string> header in request.Headers)
				httpRequest.Headers.Add(header.Key, header.Value);

			// Set request body if needed
			string requestBody = request.Body;
			if(!String.IsNullOrWhiteSpace(requestBody))
				httpRequest.Content = new StringContent(requestBody);
			
			return httpRequest;
		}
	}
}