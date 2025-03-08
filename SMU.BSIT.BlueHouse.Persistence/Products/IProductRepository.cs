namespace SMU.BSIT.BlueHouse.Persistence.Products
{
    /// <summary>
    /// Interface for product repository to handle CRUD operations for ProductModel.
    /// Note: This repository only accepts ProductModel. Ensure that ProductDTO is mapped to ProductModel before using these methods.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="product">The product to add. Ensure it is a ProductModel.</param>
        void Add(ProductModel product);

        /// <summary>
        /// Modifies an existing product in the repository.
        /// </summary>
        /// <param name="product">The product with updated information. Ensure it is a ProductModel.</param>
        void Modify(ProductModel product);

        /// <summary>
        /// Deletes a product from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        void Delete(Guid id);

        /// <summary>
        /// Retrieves a product from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to retrieve.</param>
        /// <returns>The product with the specified unique identifier.</returns>
        ProductModel GetById(Guid id);

        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <returns>An enumerable collection of all products.</returns>
        IEnumerable<ProductModel> GetAll();
    }
}
