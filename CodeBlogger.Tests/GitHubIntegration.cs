using CodeBlogger.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeBlogger.Tests
{
    [TestClass]
    public class GitHubIntegration
    {
        [TestMethod]
        public void ListAllRepos()
        {
            var token = GetConfig()["GitHub:AccessToken"];
            var client = new GitHubClient("adamfoneil", token);
            var repos = client.ListAllRepositoriesAsync().Result;
        }

        [TestMethod]
        public void ListRecentRepos()
        {
            var token = GetConfig()["GitHub:AccessToken"];
            var client = new GitHubClient("adamfoneil", token);
            var repos = client.ListRepositoriesAsync(RepoSortOptions.Pushed, SortDirection.Descending).Result;
        }

        [TestMethod]
        public void ListCommits()
        {
            var token = GetConfig()["GitHub:AccessToken"];
            var client = new GitHubClient("adamfoneil", token);
            var commits = client.ListCommitsAsync("Dapper.CX", 1).Result;
        }

        private IConfiguration GetConfig() => new ConfigurationBuilder().AddJsonFile("Config/github.json").Build();
    }
}
