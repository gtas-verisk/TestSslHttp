using System;
using System.Net.Http;

namespace TestHttpSsl
{
	public interface IMessageHandler
	{
		HttpMessageHandler GetMessageHandler();
	}
}

