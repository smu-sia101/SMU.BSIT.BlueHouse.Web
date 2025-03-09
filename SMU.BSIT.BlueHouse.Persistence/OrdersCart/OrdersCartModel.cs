namespace SMU.BSIT.BlueHouse.Persistence.OrdersCart
{
   public class OrdersCartModel : BaseModel
    {
        public Guid ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
    }
}
