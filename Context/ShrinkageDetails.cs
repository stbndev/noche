using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace noche.Context
{
    public class ShrinkageDetails : Audits
    {
        [BsonRequired]
        public int idshrinkage { get; set; }
        
        [BsonRequired]
        public int idproducts { get; set; }
        
    }
}
