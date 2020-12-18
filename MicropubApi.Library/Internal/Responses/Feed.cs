using MicropubApi.Models;
using System;

namespace MicropubApi.Library.Internal.Responses
{
    internal class Feed
    {
        public string version { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
        public string home_page_url { get; set; }
        public string feed_url { get; set; }
        public Item[] items { get; set; }
    }

    internal class Item
    {
        public string id { get; set; }
        public string title { get; set; }
        public string content_html { get; set; }
        public DateTime date_published { get; set; }
        public string url { get; set; }

        internal Entry ToEntry() => new Entry()
        { 
            Title = title,
            Body = content_html,
            DatePublished = date_published,
            Url = url
        };
    }
}
