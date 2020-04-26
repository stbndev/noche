﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace noche.Context
{
    public class Modules
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        [BsonRepresentation(BsonType.String)]
        public string name { get; set; }
    }
}
