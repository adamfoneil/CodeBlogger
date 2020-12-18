using Api.Tests.Helpers;
using GitHubApi;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.Tests
{
    [TestClass]
    public class GitHubIntegration
    {
        [TestMethod]
        public void ListPublicRepos()
        {
            var client = GetGitHubClient();
            var repos = client.ListAllPublicRepositoriesAsync().Result;
        }

        [TestMethod]
        public void ListMyRepos()
        {
            var client = GetGitHubClient();
            var repos = client.ListMyRepositoriesAsync(visibility: VisibilityOptions.Private).Result;
        }

        [TestMethod]
        public void ListRecentRepos()
        {
            var client = GetGitHubClient();
            var repos = client.ListPublicRepositoriesAsync(RepoSortOptions.Pushed, SortDirection.Descending).Result;
        }

        [TestMethod]
        public void ListPublicCommits()
        {
            var client = GetGitHubClient();
            var commits = client.ListCommitsAsync("Dapper.CX", 1).Result;
        }

        [TestMethod]
        public void ListPrivateCommits()
        {
            var client = GetGitHubClient();
            var commits = client.ListCommitsAsync("AerieHub4").Result;
        }

        private GitHubApiClient GetGitHubClient() => new GitHubApiClient(Config.Current["GitHub:UserName"], Config.Current["GitHub:AccessToken"]);     
    }
}
