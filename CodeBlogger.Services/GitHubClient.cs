using CodeBlogger.Services.Interfaces;
using CodeBlogger.Services.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBlogger.Services
{
    public class GitHubClient
    {
        private readonly string _userName;
        private readonly string _accessToken;
        private readonly IGitHubApi _api;

        public GitHubClient(string userName, string accessToken)
        {
            _userName = userName;
            _accessToken = accessToken;
            _api = RestService.For<IGitHubApi>("https://api.github.com", new RefitSettings()
            {
                AuthorizationHeaderValueGetter = async () => await Task.FromResult($"token {_accessToken}"),
                ExceptionFactory = async (message) =>
                {
                    if (!message.IsSuccessStatusCode)
                    {
                        var error = await message.Content.ReadAsStringAsync();
                        return new Exception(error);
                    }

                    return null;
                }
            });
        }

        public async Task<IReadOnlyList<Repository>> ListUserRepositoriesAsync()
        {
            List<Repository> results = new List<Repository>();
            int page = 0;

            do
            {
                page++;
                var segment = await _api.ListUserRepositoriesAsync(_userName, page);
                if (!segment.Any()) break;
                results.AddRange(segment);
            } while (true);

            return results;
        }
    }
}
