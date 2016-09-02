using System.Threading.Tasks;
using Refit;

namespace TestHttpSsl
{
	[Headers("Accept: application/vnd.github.v3+json")]
	public interface IGithubApi
	{
		[Get("/orgs/octokit/repos")]
		Task<string> GetOctokitReposAsync();
	}
}

