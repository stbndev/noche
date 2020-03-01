using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Context
{
    public class SalesDetails : Audits
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public int idsales { get; set; }
        
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
