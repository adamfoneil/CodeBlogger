using Refit;

namespace MicropubApi.Library.Internal.Responses
{
    internal class NewEntry
    {
        [AliasAs("url")]
        public string Url { get; set; }
        [AliasAs("preview")]
        public string Preview { get; set; }
    }
}
