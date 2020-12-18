using MicropubApi.Internal.Models;
using Refit;
using System.Threading.Tasks;

namespace MicropubApi.Internal.Interfaces
{
    [Headers("Authorization: Bearer")]
    internal interface IMicropubApi
    {
        [Post("/micropub")]
        Task AddPostAsync([Body]InternalEntry entry);
    }
}
