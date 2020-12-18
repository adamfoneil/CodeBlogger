using MicropubApi.Library.Internal.Responses;
using Refit;
using System.Threading.Tasks;

namespace MicropubApi.Internal.Interfaces
{
    internal interface IMicropubFeedApi
    {
        [Get("/feed.json")]
        Task<Feed> GetFeedAsync();
    }
}
