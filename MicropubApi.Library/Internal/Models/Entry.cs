using MicropubApi.Library.Models;
using Refit;

namespace MicropubApi.Internal.Models
{
    internal class InternalEntry
    {
        [AliasAs("name")]
        public string Name { get; set; }
        [AliasAs("content")]
        public string Content { get; set; }
        [AliasAs("post-status")]
        public string PostStatus { get; set; }

        internal static InternalEntry FromEntry(Entry entry) => new InternalEntry()
        {
            Name = entry.Title,
            Content = entry.Body,
            PostStatus = (entry.IsDraft) ? "draft" : string.Empty
        };        
    }
}
