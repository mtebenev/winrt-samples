namespace Mt.Common.WinRtAppCore.Imaging
{
	/// <summary>
	/// c/p from viziblr
	/// </summary>
	public class ColorException : System.Exception
	{
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		public ColorException()
		{
		}

		public ColorException(string message) : base(message)
		{
		}

		public ColorException(string message, System.Exception inner)
			: base(message, inner)
		{
		}
	}
}