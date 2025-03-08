using SMU.BSIT.BlueHouse.Bus.Enums;

namespace SMU.BSIT.BlueHouse.Bus
{
    public class BaseDTO
    {
        public Guid Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public ProductStatus Status { get; set; }
    }
}
