using Catalog.Api.Data;
using Catalog.Api.Entites;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _catalogContext;

        public ProductRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult=await _catalogContext.Products.DeleteOneAsync(p=> p.Id==id);

            return deleteResult.IsAcknowledged;
        }

        public async Task<Product> GetProductById(string id)
        {
           return await _catalogContext.Products.Find(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.Find(new BsonDocument()).ToListAsync();
        }
   
        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _catalogContext.Products
                .ReplaceOneAsync(x => x.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged;
        }

    }
}
