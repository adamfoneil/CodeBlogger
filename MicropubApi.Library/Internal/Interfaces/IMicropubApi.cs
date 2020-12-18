using MicropubApi.Internal.Requests;
using MicropubApi.Library.Internal.Responses;
using Refit;
using System.Threading.Tasks;

namespace MicropubApi.Internal.Interfaces
{
    [Headers("Authorization: Bearer")]
    internal interface IMicropubApi
    {
        [Post("/micropub")]
        Task<NewEntry> AddPostAsync([Query]InternalEntry entry);

        [Post("/micropub?action=delete&url={url}")]
        Task DeletePostAsync(string url);
    }
}
