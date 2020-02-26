using ProgBlog.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.DataAccess.Implementations
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
