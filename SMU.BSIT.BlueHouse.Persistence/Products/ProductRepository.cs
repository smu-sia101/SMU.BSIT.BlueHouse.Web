using MongoDB.Driver;

namespace SMU.BSIT.BlueHouse.Persistence.Products
{
    class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<ProductModel> _collection;
        public ProductRepository(string collectionName, string databaseName, string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);  
            var database = mongoClient.GetDatabase(databaseName);
            _collection = database.GetCollection<ProductModel>(collectionName);
        }
        public IEnumerable<ProductModel> GetAll()
        {
            List<ProductModel> products =  _collection.Find(_ => true).ToList();
            return products;
        }

        public ProductModel GetById(Guid id)
        {
            ProductModel product = _collection.Find(p => p.Id == id).FirstOrDefault();
            return product;
        }

        public void Add(ProductModel product)
        {
            _collection.InsertOne(product);
        }

        public void Modify(ProductModel product)
        {
            _collection.ReplaceOne(p => p.Id == product.Id, product);
        }

        public void Delete(Guid id)
        {
            _collection.DeleteOne(p => p.Id == id);
        }
    }
}
