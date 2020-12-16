using CodeBlogger.Services.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeBlogger.Services.Interfaces
{  
    [Headers("Authorization", "User-Agent: CodeBloggerAPI", "Accept: application/vnd.github.v3+json")]
    internal interface IGitHubApi
    {
        [Get("/users/{userName}/repos?page={page}&sort={sort}&direction={direction}")]
        Task<IReadOnlyList<Repository>> ListUserRepositoriesAsync(string userName, string sort, string direction, int page);

        [Get("/repos/{userName}/{repoName}/commits?page={page}")]
        Task<IReadOnlyList<CommitHeader>> ListCommitHeadersAsync(string userName, string repoName, int page = 1);
    }
}
