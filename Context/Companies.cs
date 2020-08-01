using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Context
{
    public class Companies
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string name { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string shortname { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string logo { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string email { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string rfc { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.Int32)]
        public int idcstatus { get; set; }
        
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string address { get; set; }
    }
}
