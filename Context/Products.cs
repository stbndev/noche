using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Context
{
    public class Products
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public int sequence_value { get; set; }
        [BsonRequired]
        public string name { get; set; }
        [BsonRequired]
        public string barcode { get; set; }
        [BsonRequired]
        public int idcstatus { get; set; }
        [BsonRequired]
        public decimal unitary_cost { get; set; }
        [BsonRequired]
        public decimal unitary_price { get; set; }
        [BsonRequired]
        public decimal existence { get; set; }

        public string pathimg { get; set; }

        [BsonRequired]
        [BsonDefaultValue(0)]
        public int idcompany { get; set; }


        [BsonRequired]
        [BsonDefaultValue("system admin")]
        public string maker { get; set; }
        [BsonDefaultValue(0)]
        public int date_add { get; set; }
        [BsonDefaultValue(0)]
        public int date_set { get; set; }
    }
}
