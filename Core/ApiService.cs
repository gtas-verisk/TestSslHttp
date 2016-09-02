using System;
using System.Net.Http;
using Refit;

namespace TestHttpSsl
{
	public class ApiService
	{
		private Lazy<IApi> _lazyApi;
		private Lazy<IGithubApi> _lazyGithubApi;

		public IApi WebApi
		{
			get
			{
				return _lazyApi.Value;
			}
		}

		public IGithubApi GithubWebApi
		{
			get
			{
				return _lazyGithubApi.Value;
			}
		}

		protected HttpClient ApiHttpClient
		{
			get; set;
		}

		protected HttpClient GithubApiHttpClient
		{
			get; set;
		}

		public ApiService(IMessageHandler messageHandler)
		{
			ApiHttpClient = new HttpClient(messageHandler.GetMessageHandler())
			{
				BaseAddress = new Uri("https://flightkitapi.iso.com/sync")
			};

			GithubApiHttpClient = new HttpClient(messageHandler.GetMessageHandler())
			{
				BaseAddress = new Uri("https://api.github.com")
			};

			_lazyApi = new Lazy<IApi>(() => RestService.For<IApi>(ApiHttpClient));
			_lazyGithubApi = new Lazy<IGithubApi>(() => RestService.For<IGithubApi>(GithubApiHttpClient));
		}
	}
}

