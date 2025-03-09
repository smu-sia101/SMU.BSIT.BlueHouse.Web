using Microsoft.AspNetCore.Mvc;
using SMU.BSIT.BlueHouse.Bus.Services.Products;
using SMU.BSIT.BlueHouse.Web.Models.Admin.Products;

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

            ProductListViewModel model = new ProductListViewModel();
            model.Products = productViewModels;
            model.CurrentPage = currentPage;
            model.TotalPages = productViewModels.Count / 10;
            model.Count = productViewModels.Count;
            
            return View("~/Views/Admin/Products/Index.cshtml", model);
        }

        [HttpGet("{id}")]
        public IActionResult Details(Guid id)
        {
            try
            {
                ProductViewModel model = new ProductViewModel();
                ProductDTO product = _productService.GetById(id);

                if (product != null)
                {
                    model = MapDTOtoViewModel(product);
                }
                return View("~/Views/Admin/Products/Details.cshtml", model);
            }
            catch (Exception)
            {
                return View("~/Views/Admin/Products/Details.cshtml");
            }
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

        [HttpGet("{id}/edit")]
        public IActionResult Edit(Guid id)
        {
            var product = _productService.GetById(id);
            if (product != null) {
                ProductViewModel model = MapDTOtoViewModel(product);
                return View("~/Views/Admin/Products/Edit.cshtml", model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Products/Edit.cshtml", model);
            }

            try
            {
                ProductDTO product = await MapModelToDTO(model);
                _productService.Modify(product);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Details", new { id = model.Id });
        }

        private static async Task<ProductDTO> MapModelToDTO(ProductViewModel model)
        {
            ProductDTO dto = new ProductDTO
            {
                Id = model.Id,
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
                CoverImage = ConvertToIFormFile(productDTO.CoverImage, "CoverImage", productDTO.CoverImageType),
                CoverImageUrl = ConvertToDataUrl(productDTO.CoverImage, productDTO.CoverImageType)
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

        public static string ConvertToDataUrl(byte[] imageBytes, string contentType)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                return ""; // Return empty string if no image

            string base64String = Convert.ToBase64String(imageBytes);
            return $"data:{contentType};base64,{base64String}";
        }
    }
}
