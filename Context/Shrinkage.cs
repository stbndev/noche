using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Context
{
    public class Shrinkage : Audits
    {
        [BsonRequired]
        [BsonDefaultValue(0)]
        public decimal total { get; set; }
    }
}
