using MicropubApi.Library.Models;
using Refit;

namespace MicropubApi.Internal.Requests
{
    internal class InternalEntry
    {
        [AliasAs("name")]
        public string Name { get; set; }
        [AliasAs("content")]
        public string Content { get; set; }
        [AliasAs("post-status")]
        public string PostStatus { get; set; }

        internal static InternalEntry FromEntry(NewEntry entry) => new InternalEntry()
        {
            Name = entry.Title,
            Content = entry.Body,
            PostStatus = (entry.IsDraft) ? "draft" : string.Empty
        };        
    }
}
