﻿using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using noche.Config;

namespace noche.Repository
{
    public class EntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        [BsonDefaultValue(0)]
        public int idcompany { get; set; }

        [BsonRequired]
        [BsonDefaultValue(0)]
        public int sequence_value { get; set; }

        [BsonRequired]
        [BsonDefaultValue("noche")]
        public string maker { get; set; } = "noche";

        [BsonRequired]
        [BsonDefaultValue(0)]
        public int date_add { get; set; } = 0;

        [BsonRequired]
        [BsonDefaultValue(0)]
        public int date_set { get; set; } = 0;
    }
    public class MongoDbRepository<T> where T : EntityBase
    {
        private IMongoDatabase _database = null;
        private IMongoCollection<T> _collection;
        private readonly IOptions<Mongosettings> _mongosettings;


        public MongoDbRepository(IOptions<Mongosettings> settings)
        {
            _mongosettings = settings;
            var client = new MongoClient(_mongosettings.Value.ConnectionString);
            _database = client.GetDatabase(_mongosettings.Value.DatabaseName);
            _collection = _database.GetCollection<T>(typeof(T).Name);
        }
    }
}
