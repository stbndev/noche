using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using noche.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Context
{
    public class Products : EntityBase
    {


        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string name { get; set; }
        
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string barcode { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string pathimg { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.Int32)]
        public int idcstatus { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal unitary_price { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal unitary_cost { get; set; }
        
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal existence { get; set; }

    }
}
