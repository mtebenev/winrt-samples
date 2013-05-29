using System.IO;
using System.Threading.Tasks;

namespace Mt.Common.AppCore.RestHttpClient
{
	/// <summary>
	/// Web service HTTP client wraps basic WS calls functionality
	/// </summary>
	public interface IWsHttpClient
	{
		Task<Stream> GetStreamAsync(IWsRequest request);
		Task<string> GetStringAsync(IWsRequest request);
		Task<string> PostStringAsync(IWsRequest request);
		Task<Stream> PostStreamAsync(IWsRequest request);
	}
}
