using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace noche.Context
{
    public class Sales : Audits
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public decimal total { get; set; }

        public List<SalesDetails> details { get; set; }

    }

    public class SalesDetails
    {
        [BsonRequired]
        public decimal unitary_cost { get; set; }
        [BsonRequired]
        public decimal unitary_price { get; set; }
        [BsonRequired]
        public int quantity { get; set; }
        [BsonRequired]
        public int idproducts { get; set; }
    }
}
