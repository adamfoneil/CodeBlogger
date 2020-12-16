using CodeBlogger.Services.Interfaces;
using Refit;

namespace CodeBlogger.Services
{
    public class GitHubClient
    {
        private readonly string _accessToken;
        private readonly IGitHubApi _api;

        public GitHubClient(string accessToken)
        {
            _accessToken = accessToken;
            _api = RestService.For<IGitHubApi>("https://github.com");
        }
    }
}
