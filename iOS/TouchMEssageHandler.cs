using System;
using System.Net.Http;

namespace TestHttpSsl.iOS
{
	public class TouchMessageHandler : IMessageHandler
	{
		public HttpMessageHandler GetMessageHandler()
		{
			return new NSUrlSessionHandler();
		}
	}
}

