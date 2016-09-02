using System.Net.Http;
using Xamarin.Android.Net;

namespace TestHttpSsl
{
	public class AndroidMessageHandler : IMessageHandler
	{
		public HttpMessageHandler GetMessageHandler()
		{
			return new AndroidClientHandler();
		}
	}
}

