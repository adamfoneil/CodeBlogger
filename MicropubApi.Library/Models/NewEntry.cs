using System;

namespace MicropubApi.Library.Models
{
    public class NewEntry
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsDraft { get; set; }
    }
}
