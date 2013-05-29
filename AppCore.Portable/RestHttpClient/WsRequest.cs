using System.Collections.Generic;

namespace Mt.Common.AppCore.RestHttpClient
{
	/// <summary>
	/// REST client request implementation
	/// </summary>
	public class WsRequest : IWsRequest
	{
		private readonly Dictionary<string, string> _parameters;
		private readonly Dictionary<string, string> _headers;

		public WsRequest()
		{
			_parameters = new Dictionary<string, string>();
			_headers = new Dictionary<string, string>();
		}

		public IEnumerable<KeyValuePair<string, string>> Parameters
		{
			get
			{
				return _parameters;
			}
		}

		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return _headers;
			}
		}

		public virtual string Body
		{
			get
			{
				return null;
			}
		}

		public void AddParameter(string name, string value)
		{
			_parameters.Add(name, value);
		}

		public void AddHeader(string name, string value)
		{
			_headers.Add(name, value);
		}
	}
}