namespace CodeBlogger.Services.Models
{
    public class Entry
    {
        public string Title { get; set; }
        public string RepoUrl { get; set; }
        public string Tags { get; set; }
        public string Commits { get; set; }
        public string MarkdownBody { get; set; }

        public string Filename() => Title.ToLower().Replace(" ", "-").Substring(0, 30);
    }
}
