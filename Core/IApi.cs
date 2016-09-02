using System;
using System.Threading.Tasks;
using Refit;

namespace TestHttpSsl
{
	[Headers("access_token: a0ee216f-621d-4065-a171-bf49c51af636")]
	public interface IApi
	{
		[Get("/Order")]
		Task<string> GetInitialOrUpdatedData(string clientId, long workerId);
	}
}

