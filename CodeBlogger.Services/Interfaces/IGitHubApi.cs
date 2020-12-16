using CodeBlogger.Services.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeBlogger.Services.Interfaces
{
    public interface IGitHubApi
    {
        [Get("/repos/{userName}")]
        Task<IReadOnlyList<Repository>> GetRepositoriesAsync(string userName, string accessToken);
    }
}
