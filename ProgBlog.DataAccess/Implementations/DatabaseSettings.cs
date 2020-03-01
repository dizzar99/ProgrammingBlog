using ProgBlog.DataAccess.Interfaces;

namespace ProgBlog.DataAccess.Implementations
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
