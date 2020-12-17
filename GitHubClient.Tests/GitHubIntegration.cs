using GitHubApi;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeBlogger.Tests
{
    [TestClass]
    public class GitHubIntegration
    {
        [TestMethod]
        public void ListPublicRepos()
        {
            var client = GetClient();
            var repos = client.ListAllPublicRepositoriesAsync().Result;
        }

        [TestMethod]
        public void ListMyRepos()
        {
            var client = GetClient();
            var repos = client.ListMyRepositoriesAsync(visibility: VisibilityOptions.Private).Result;
        }

        [TestMethod]
        public void ListRecentRepos()
        {
            var client = GetClient();
            var repos = client.ListPublicRepositoriesAsync(RepoSortOptions.Pushed, SortDirection.Descending).Result;
        }

        [TestMethod]
        public void ListPublicCommits()
        {
            var client = GetClient();
            var commits = client.ListCommitsAsync("Dapper.CX", 1).Result;
        }

        [TestMethod]
        public void ListPrivateCommits()
        {
            var client = GetClient();
            var commits = client.ListCommitsAsync("AerieHub4").Result;
        }

        private GitHubApiClient GetClient() => new GitHubApiClient(Config["GitHub:UserName"], Config["GitHub:AccessToken"]);

        private IConfiguration Config { get => new ConfigurationBuilder().AddJsonFile("Config/github.json").Build(); }
    }
}
