using System;

namespace MicropubApi.Models
{
    public class Entry
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
