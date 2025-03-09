using MongoDB.Driver;

namespace SMU.BSIT.BlueHouse.Persistence.OrdersCart
{
    public class OrdersCartRepository : IOrdersCartRepository
    {
        private readonly IMongoCollection<OrdersCartModel> _collection;

        public OrdersCartRepository(string collectionName, string databaseName, string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);
            _collection = database.GetCollection<OrdersCartModel>(collectionName);
        }

        public IEnumerable<OrdersCartModel> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public OrdersCartModel GetById(Guid id)
        {
            return _collection.Find(orderCart => orderCart.Id == id).FirstOrDefault();
        }

        public void Add(OrdersCartModel orderCart)
        {
            _collection.InsertOne(orderCart);
        }

        public void Modify(OrdersCartModel orderCart)
        {
            _collection.ReplaceOne(cart => cart.Id == orderCart.Id, orderCart);
        }

        public void Delete(Guid id)
        {
            _collection.DeleteOne(orderCart => orderCart.Id == id);
        }
    }
}
