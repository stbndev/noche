using MongoDB.Bson.Serialization.Attributes;
using noche.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Context
{
    public class Shrinkages : EntityBase
    {
        [BsonRequired]
        [BsonDefaultValue(0)]
        public decimal total { get; set; }

        public List<ShrinkageDetails> details { get; set; }

        [BsonRequired]
        [BsonDefaultValue(0)]
        public int idcstatus { get; set; }
    }

    public class ShrinkageDetails
    {
        [BsonRequired]
        public decimal unitary_cost { get; set; }
        [BsonRequired]
        public int idproducts { get; set; }
        [BsonRequired]
        public decimal quantity { get; set; }

    }
}
