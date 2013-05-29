using System.Collections.Generic;

namespace Mt.Common.AppCore.RestHttpClient
{
	/// <summary>
	/// Encapsulates neccessary information for a REST request
	/// </summary>
	public interface IWsRequest
	{
		/// <summary>
		/// Parameters of the request
		/// </summary>
		IEnumerable<KeyValuePair<string, string>> Parameters { get; }

		/// <summary>
		/// HTTP headers of the request
		/// </summary>
		IEnumerable<KeyValuePair<string, string>> Headers { get; }

		/// <summary>
		/// Body of the request (approriate when PUT method used)
		/// </summary>
		string Body { get; }

		/// <summary>
		/// Adds another parameter to the request
		/// </summary>
		void AddParameter(string name, string value);

		/// <summary>
		/// Adds another HTTP header to the request
		/// </summary>
		void AddHeader(string name, string value);
	}
}