using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Api.Entites
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string name { get; set; }
        public string catagory { get; set; }
        public decimal price { get; set; }


    }
}
