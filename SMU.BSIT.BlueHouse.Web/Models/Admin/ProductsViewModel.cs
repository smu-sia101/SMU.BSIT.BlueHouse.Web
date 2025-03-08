namespace SMU.BSIT.BlueHouse.Web.Models.Admin
{
    public class ProductsViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public int Count { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
