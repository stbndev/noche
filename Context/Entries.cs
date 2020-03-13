using MongoDB.Bson.Serialization.Attributes;
using noche.Repository;

namespace noche.Context
{
    public class Entries : EntityBase
    {
        [BsonRequired]
        public int idproducts { get; set; }

        [BsonRequired]
        public decimal total { get; set; }

        [BsonRequired]
        public decimal unitary_cost { get; set; }

        [BsonRequired]
        public decimal quantity { get; set; }

        [BsonRequired]
        public int idcstatus { get; set; }
    }
}
