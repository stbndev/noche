using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using noche.Repository;

namespace noche.Context
{
    public class Entries : EntityBase
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.Int32)]
        public int idproducts { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal total { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal unitary_cost { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal quantity { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.Int32)]
        public int idcstatus { get; set; }
    }
}
