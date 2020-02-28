using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace noche.Models
{
    public class Products
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int sequence_value { get; set; }

        public string name { get; set; }
        public string barcode { get; set; }
        public int idcstatus { get; set; }
        public decimal unitary_cost { get; set; }
        public decimal unitary_price { get; set; }
        public decimal existence { get; set; }
        public string pathimg { get; set; }
        public string maker { get; set; }
        public int date_add { get; set; }
        public int date_set { get; set; }
    }
}
