﻿using SMU.BSIT.BlueHouse.Bus.Enums;
using SMU.BSIT.BlueHouse.Bus.Services.Security;
using SMU.BSIT.BlueHouse.Persistence.Products;

namespace SMU.BSIT.BlueHouse.Bus.Services.Products
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IUserService _userService;

        public ProductService(IProductRepository productRepository, IUserService userService)
        {
            _productRepository = productRepository;
            _userService = userService;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            if (_userService.HasPermission(Permission.Read, typeof(ProductModel)))
            {
                List<ProductDTO> productDTOs = new List<ProductDTO>();
                IEnumerable<ProductModel> products = _productRepository.GetAll();

                foreach (var product in products)
                {
                    ProductDTO mapped = MapModelToDTO(product);
                    productDTOs.Add(mapped);
                }

                return productDTOs;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public ProductDTO GetById(Guid id)
        {
            if (_userService.HasPermission(Permission.Read, typeof(ProductModel)))
            {
                ProductModel product = _productRepository.GetById(id);
                ProductDTO mapped = MapModelToDTO(product);
                return mapped;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
        public void Add(ProductDTO product)
        {
            if (_userService.HasPermission(Permission.Create,typeof(ProductModel)))
            {
                ProductModel productModel = MapDTOtoModel(product);

                _productRepository.Add(productModel);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public void Delete(Guid id)
        {
            if (_userService.HasPermission(Permission.Delete, typeof(ProductModel)))
            {
                _productRepository.Delete(id);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public void Modify(ProductDTO productUpdate)
        {
            if (_userService.HasPermission(Permission.Update, typeof(ProductModel)))
            {
                ProductDTO existingProduct = GetById(productUpdate.Id);
                if (existingProduct != null)
                {
                    if (productUpdate.CoverImage == null)
                    {
                        productUpdate.CoverImage = existingProduct.CoverImage;
                        productUpdate.CoverImageType = existingProduct.CoverImageType;
                    }

                    ProductModel productModel = MapDTOtoModel(productUpdate);
                    _productRepository.Modify(productModel);
                }
                else
                {
                    throw new NotSupportedException($"Product with id {productUpdate.Id} is not existing!");
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        private ProductModel MapDTOtoModel(ProductDTO product)
        {
            if (product == null)
            {
               throw new ArgumentNullException(nameof(product));
            }

            return new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Category = product.Category,
                Manufacturer = product.Manufacturer,
                DateCreated = DateTimeOffset.UtcNow,
                DateModified = DateTimeOffset.UtcNow,
                CreatedBy = _userService.Username,
                ModifiedBy = _userService.Username,
                Status = (int)product.Status,
                CoverImage = product.CoverImage,
                CoverImageType = product.CoverImageType
            };
        }

        private static ProductDTO MapModelToDTO(ProductModel product)
        {
            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Manufacturer = product.Manufacturer,
                Category = product.Category,
                CreatedBy = product.CreatedBy,
                ModifiedBy = product.ModifiedBy,
                DateCreated = product.DateCreated,
                DateModified = product.DateModified,
                Status = (ProductStatus)product.Status,
                CoverImage = product.CoverImage,
                CoverImageType = product.CoverImageType
            };
        }
    }
}
