using Microsoft.AspNetCore.Mvc;
using SharpCompress.Compressors.Xz;
using SMU.BSIT.BlueHouse.Bus.Services.Products;
using SMU.BSIT.BlueHouse.Web.Models.Admin;
using System.IO;

namespace SMU.BSIT.BlueHouse.Web.Controllers
{
    [Route("admin/products")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] int page)
        {
            int currentPage = page;
            IEnumerable<ProductDTO> productDTOs = _productService.GetAll();
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            foreach (var productDTO in productDTOs)
            {
                productViewModels.Add(MapDTOtoViewModel(productDTO));
            }

            ProductsViewModel model = new ProductsViewModel();
            model.Products = productViewModels;
            model.CurrentPage = currentPage;
            model.TotalPages = productViewModels.Count / 10;
            model.Count = productViewModels.Count;
            
            return View("~/Views/Admin/Products/Index.cshtml", model);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Products/Create.cshtml");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Products/Create.cshtml", model);
            }

            ProductDTO dto = await MapModelToDTO(model);
            _productService.Add(dto);


            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var product = _productService.GetById(id);
            if (product != null) {
                ProductViewModel model = MapDTOtoViewModel(product);
                return View("~/Views/Admin/Products/Edit.cshtml", model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Products/Edit.cshtml", model);
            }

            return RedirectToAction("Index");
        }

        private static async Task<ProductDTO> MapModelToDTO(ProductViewModel model)
        {
            ProductDTO dto = new ProductDTO
            {
                Name = model.Name,
                Description = model.Description,
                Category = model.Category,
                Stock = model.Stock,
                Price = model.Price,
                Manufacturer = model.Manufacturer,
            };

            if(model.CoverImage != null)
                using (var coverImageStream = new MemoryStream())
                {
                    await model.CoverImage.CopyToAsync(coverImageStream);
                    dto.CoverImage = coverImageStream.ToArray();
                    dto.CoverImageType = model.CoverImage.ContentType;
                }

            return dto;
        }

        private static ProductViewModel MapDTOtoViewModel(ProductDTO productDTO)
        {
            return new ProductViewModel()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Category = productDTO.Category,
                Stock = productDTO.Stock,
                Price = productDTO.Price,
                Manufacturer = productDTO.Manufacturer,
                CoverImage = ConvertToIFormFile(productDTO.CoverImage, "CoverImage", productDTO.CoverImageType)
                //TODO: Add Image Url
            };
        }

        private static IFormFile ConvertToIFormFile(byte[] fileBytes, string fileName, string contentType)
        {
            if (fileBytes == null)
                return default;

            var stream = new MemoryStream(fileBytes);
            return new FormFile(stream, 0, fileBytes.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }

    }
}
