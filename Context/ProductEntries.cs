using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Context
{
    public class ProductEntries : Audits
    {
        [BsonRequired]
        public decimal total { get; set; }
    }
}
