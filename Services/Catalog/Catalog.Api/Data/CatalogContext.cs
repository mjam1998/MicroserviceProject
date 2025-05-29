using Catalog.Api.Entites;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext
    {

        public IMongoCollection<Product> Products { get; set; }
        public CatalogContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDBSetting:ConnectionString"];
            var databaseName= configuration["MongoDBSetting:DatabaseName"];
            var client =new MongoClient(connectionString);
            var database=client.GetDatabase(databaseName);
            Products = database.GetCollection<Product>("Products");
        }
    }
}
