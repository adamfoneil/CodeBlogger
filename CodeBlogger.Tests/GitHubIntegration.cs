using CodeBlogger.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeBlogger.Tests
{
    [TestClass]
    public class GitHubIntegration
    {
        [TestMethod]
        public void ListRepos()
        {
            var token = GetConfig()["GitHub:AccessToken"];
            var client = new GitHubClient("adamfoneil", token);
            var repos = client.ListUserRepositoriesAsync().Result;
        }

        private IConfiguration GetConfig() => new ConfigurationBuilder().AddJsonFile("Config/github.json").Build();
    }
}
