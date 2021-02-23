using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using noche.Repository;

namespace noche.Context
{
    public class Users : EntityBase
    {
        
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string name { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string email { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string password { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string idrol { get; set; }
        
    }


    

    
}
