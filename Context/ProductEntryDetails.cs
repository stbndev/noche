using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Context
{
    public class ProductEntryDetails 
    {
        [BsonRequired]
        public int idproductentries { get; set; }
        [BsonRequired]
        public decimal unitary_cost { get; set; }
        [BsonRequired]
        public decimal quantity { get; set; }
        [BsonRequired]
        public int idproducts { get; set; }
    }
}
