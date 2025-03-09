using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SMU.BSIT.BlueHouse.Bus.Services.Products;
using SMU.BSIT.BlueHouse.Web.Models;
using SMU.BSIT.BlueHouse.Web.Models.Admin.Products;

namespace SMU.BSIT.BlueHouse.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;

    public HomeController(ILogger<HomeController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public IActionResult Index()
    {
        var productDTOs = _productService.GetAll();
        var productViewModels = productDTOs.Select(MapDTOtoViewModel).ToList();

        var model = new ProductListViewModel { Products = productViewModels };

        return View(model);
    }

    [HttpPost]
    public IActionResult AddToCart(Guid id)
    {
        // Logic to add product to cart (implement as needed)
        _logger.LogInformation($"Product {id} added to cart.");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult BuyNow(Guid id)
    {
        // Logic to handle direct purchase (implement as needed)
        _logger.LogInformation($"Product {id} purchased.");
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private static ProductViewModel MapDTOtoViewModel(ProductDTO productDTO)
    {
        return new ProductViewModel
        {
            Id = productDTO.Id,
            Name = productDTO.Name,
            Price = productDTO.Price,
            Category = productDTO.Category,
            CoverImageUrl = ConvertToDataUrl(productDTO.CoverImage, productDTO.CoverImageType)
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