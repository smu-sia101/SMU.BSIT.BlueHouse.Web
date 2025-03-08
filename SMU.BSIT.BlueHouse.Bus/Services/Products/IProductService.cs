namespace SMU.BSIT.BlueHouse.Bus.Services.Products
{
    /// <summary>
    /// Interface for product service to handle CRUD operations for ProductDTO.
    /// Note: This service uses ProductDTO. Ensure that ProductDTO is mapped to ProductModel when interacting with the repository.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>An enumerable collection of all products as ProductDTO.</returns>
        IEnumerable<ProductDTO> GetAll();

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to retrieve.</param>
        /// <returns>The product with the specified unique identifier as ProductDTO.</returns>
        ProductDTO GetById(Guid id);

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">The product to add. Ensure it is a ProductDTO.</param>
        void Add(ProductDTO product);

        /// <summary>
        /// Modifies an existing product.
        /// </summary>
        /// <param name="product">The product with updated information. Ensure it is a ProductDTO.</param>
        void Modify(ProductDTO product);

        /// <summary>
        /// Deletes a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        void Delete(Guid id);
    }
}
