using HttpTracer;
using MicropubApi.Internal.Interfaces;
using MicropubApi.Internal.Models;
using MicropubApi.Library.Models;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicropubApi
{
    public class MicropubApiClient
    {
        private readonly IMicropubApi _api;
        private readonly string _token;

        private static HttpClient _httpClient = new HttpClient(new HttpTracerHandler(HttpMessageParts.All))
        {
            BaseAddress = new Uri("https://micro.blog")
        };

        public MicropubApiClient(string token)
        {
            _token = token;
            _api = RestService.For<IMicropubApi>(_httpClient, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = async () => await Task.FromResult(_token),
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

        public async Task AddPostAsync(Entry entry) => await _api.AddPostAsync(InternalEntry.FromEntry(entry));
    }
}
