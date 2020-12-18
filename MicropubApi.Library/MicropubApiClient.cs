using MicropubApi.Internal.Interfaces;
using MicropubApi.Internal.Requests;
using MicropubApi.Library.Models;
using MicropubApi.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicropubApi
{
    public class MicropubApiClient
    {
        private readonly IMicropubApi _api;
        private readonly IMicropubFeedApi _feedApi;

        private readonly string _token;        

        public MicropubApiClient(string userName, string token)
        {
            _token = token;            

            _api = RestService.For<IMicropubApi>("https://micro.blog", new RefitSettings()
            {
                AuthorizationHeaderValueGetter = async () => await Task.FromResult(_token),
                ExceptionFactory = GetException
            });

            _feedApi = RestService.For<IMicropubFeedApi>($"https://{userName}.micro.blog", new RefitSettings()
            {
                ExceptionFactory = GetException
            });
        }

        public async Task<(string Url, string Preview)> AddPostAsync(NewEntry entry)
        {
            var result = await _api.AddPostAsync(InternalEntry.FromEntry(entry));
            return (result.Url, result.Preview);
        }

        public async Task DeletePostAsync(string url) => await _api.DeletePostAsync(url);

        public async Task<IReadOnlyList<Entry>> GetFeedAsync()
        {
            var feed = await _feedApi.GetFeedAsync();
            var result = feed.items.Select(item => item.ToEntry());
            return result.ToList();
        }

        private static async Task<Exception> GetException(HttpResponseMessage message)
        {
            if (!message.IsSuccessStatusCode)
            {
                var error = await message.Content.ReadAsStringAsync();
                return new Exception(error);
            }

            return null;
        }
    }
}
