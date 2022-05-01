using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaoToAngularAndCSharp.DAL.Repostories
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionURL { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
        public string CollectionName2 { get; set; } = null!;

    }
}
