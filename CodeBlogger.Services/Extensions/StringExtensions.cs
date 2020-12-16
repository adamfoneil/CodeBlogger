using System.IO;
using System.Text;

namespace CodeBlogger.Services.Extensions
{
    internal static class StringExtensions
    {
        internal static MemoryStream ToMemoryStream(this string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            return new MemoryStream(bytes);
        }
    }
}
