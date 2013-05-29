namespace Mt.Common.AppCore.RestHttpClient
{
	/// <summary>
	/// XML web service request implementation
	/// </summary>
	public class XmlWsRequest : WsRequest
	{
		private readonly string _requestBody;

		public XmlWsRequest(string requestBody)
		{
			_requestBody = requestBody;
		}

		public override string Body
		{
			get
			{
				return _requestBody;
			}
		}
	}
}