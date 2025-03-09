using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SMU.BSIT.BlueHouse.Persistence
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public int Status { get; set; }

        public BaseModel()
        {
            if(Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
        }
    }
}
