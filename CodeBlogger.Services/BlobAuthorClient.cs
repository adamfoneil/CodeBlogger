namespace CodeBlogger.Services
{
    public class BlobAuthorClient
    {
        private readonly string _connectionString;
        private readonly string _container;
        private readonly string _basePrefix;

        public BlobAuthorClient(string connectionString, string container, string basePrefix)
        {
            _connectionString = connectionString;
            _container = container;
            _basePrefix = basePrefix;
        }


    }
}
