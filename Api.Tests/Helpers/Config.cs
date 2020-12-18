using Microsoft.Extensions.Configuration;

namespace Api.Tests.Helpers
{
    public static class Config
    {
        public static IConfiguration Current
        {
            get => new ConfigurationBuilder()
                .AddJsonFile("Config/github.json")
                .AddJsonFile("Config/micro.blog.json").Build();
        }
    }
}
