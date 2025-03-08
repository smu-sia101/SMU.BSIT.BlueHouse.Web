namespace SMU.BSIT.BlueHouse.Persistence.Products
{
    public class ProductModel : BaseModel
    {
        public required string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public short Stock { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public byte[] CoverImage { get; set; }
        public string CoverImageType { get; set; }
    }
}
