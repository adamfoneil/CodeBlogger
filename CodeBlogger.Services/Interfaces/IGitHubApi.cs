using CodeBlogger.Services.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeBlogger.Services.Interfaces
{        
    [Headers("Authorization", "User-Agent: CodeBloggerAPI")]
    public interface IGitHubApi
    {
        [Get("/users/{userName}/repos?page={page}")]
        Task<IReadOnlyList<Repository>> ListUserRepositoriesAsync(string userName, int page = 1);
    }
}
