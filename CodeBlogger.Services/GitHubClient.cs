using CodeBlogger.Services.Interfaces;
using CodeBlogger.Services.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBlogger.Services
{
    public enum RepoSortOptions
    {
        Created,
        Updated,
        Pushed,
        FullName
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

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

        public async Task<IReadOnlyList<Repository>> ListRepositoriesAsync(
            RepoSortOptions sort = RepoSortOptions.Pushed, SortDirection direction = SortDirection.Descending, int page = 1) => 
            await _api.ListUserRepositoriesAsync(_userName, SortOptionText(sort), SortDirectionText(direction), page);

        public async Task<IReadOnlyList<Repository>> ListAllRepositoriesAsync(
            RepoSortOptions sort = RepoSortOptions.Pushed, SortDirection direction = SortDirection.Descending)
        {
            List<Repository> results = new List<Repository>();
            int page = 0;

            do
            {
                page++;
                var segment = await ListRepositoriesAsync(sort, direction, page);
                if (!segment.Any()) break;
                results.AddRange(segment);
            } while (true);

            return results;
        }

        public async Task<IReadOnlyList<CommitHeader>> ListCommitsAsync(string repoName, int page = 1) => await _api.ListCommitHeadersAsync(_userName, repoName, page);

        private static string SortOptionText(RepoSortOptions sort) =>
            (sort == RepoSortOptions.Created) ? "created" :
            (sort == RepoSortOptions.FullName) ? "full_name" :
            (sort == RepoSortOptions.Pushed) ? "pushed" :
            (sort == RepoSortOptions.Updated) ? "updated" :
            throw new Exception($"Unknown sort option {sort}");

        private static string SortDirectionText(SortDirection direction) =>
            (direction == SortDirection.Ascending) ? "asc" :
            (direction == SortDirection.Descending) ? "desc" :
            throw new Exception($"Unknown sort direction {direction}");
    }        
}
