using Api.Tests.Helpers;
using MicropubApi;
using MicropubApi.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Api.Tests
{
    [TestClass]
    public class MicropubTests
    {
        [TestMethod]
        public void AddAndDeletePost()
        {
            var client = GetClient();
            var result = client.AddPostAsync(new NewEntry()
            {
                Title = "sample post",
                Body = "burn after reading",
                IsDraft = true
            }).Result;
            
            client.DeletePostAsync(result.Url).Wait();

            var feed = client.GetFeedAsync().Result;

            // sample post should be gone
            Assert.IsTrue(!feed.Any(e => e.Url.Equals(result.Url)));
        }

        private MicropubApiClient GetClient() => new MicropubApiClient(Config.Current["Micro.Blog:UserName"], Config.Current["Micro.Blog:AccessToken"]);
    }
}
