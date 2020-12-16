using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CodeBlogger.Services.Extensions;
using CodeBlogger.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBlogger.Services
{
    public enum EntryVisibility
    {
        Hidden,
        Public
    }

    public class BlogAuthorClient
    {
        private readonly string _connectionString;
        private readonly string _container;
        private readonly string _basePrefix;

        public BlogAuthorClient(string connectionString, string container, string basePrefix)
        {
            _connectionString = connectionString;
            _container = container;
            _basePrefix = basePrefix;
        }

        public async Task SaveAsync(Entry entry, EntryVisibility visibility, string name = null)
        {
            if (entry is null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            var client = new BlobClient(_connectionString, _container, BuildPath(_basePrefix, $"{name ?? entry.Filename()}.md"));

            using (var stream = entry.MarkdownBody.ToMemoryStream())
            {
                await client.UploadAsync(stream, new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders()
                    {
                        ContentType = "text/markdown"
                    },
                    Metadata = new Dictionary<string, string>()
                    {
                        [nameof(Entry.Title)] = entry.Title,
                        [nameof(Entry.RepoUrl)] = entry.RepoUrl,
                        [nameof(Entry.Tags)] = entry.Tags,
                        [nameof(Entry.Commits)] = entry.Commits,
                        ["Visibility"] = visibility.ToString()
                    }
                });
            }
        }
        
        private static string BuildPath(params string[] parts)
        {
            return string.Join("/", parts.Where(path => !string.IsNullOrEmpty(path)));
        }
    }
}
