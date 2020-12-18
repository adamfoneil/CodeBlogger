using Api.Tests.Helpers;
using MicropubApi;
using MicropubApi.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.Tests
{
    [TestClass]
    public class MicropubTests
    {
        [TestMethod]
        public void AddAndDeletePost()
        {
            var client = GetClient();
            client.AddPostAsync(new Entry()
            {
                Title = "sample post",
                Body = "burn after reading",
                IsDraft = true
            }).Wait();
        }

        private MicropubApiClient GetClient() => new MicropubApiClient(Config.Current["Micro.Blog:AccessToken"]);
    }
}
