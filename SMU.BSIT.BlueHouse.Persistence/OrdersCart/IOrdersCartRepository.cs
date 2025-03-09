namespace SMU.BSIT.BlueHouse.Persistence.OrdersCart
{
    /// <summary>
    /// Interface for Order Cart Repository to handle CRUD operations.
    /// </summary>
    public interface IOrdersCartRepository
    {
        /// <summary>
        /// Gets all order carts.
        /// </summary>
        /// <returns>A collection of OrderCartModel.</returns>
        IEnumerable<OrdersCartModel> GetAll();

        /// <summary>
        /// Gets an order cart by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the order cart.</param>
        /// <returns>The OrderCartModel with the specified identifier.</returns>
        OrdersCartModel GetById(Guid id);

        /// <summary>
        /// Adds a new order cart.
        /// </summary>
        /// <param name="orderCart">The order cart to add.</param>
        void Add(OrdersCartModel orderCart);

        /// <summary>
        /// Modifies an existing order cart.
        /// </summary>
        /// <param name="orderCart">The order cart to modify.</param>
        void Modify(OrdersCartModel orderCart);

        /// <summary>
        /// Deletes an order cart by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the order cart to delete.</param>
        void Delete(Guid id);
    }
}
