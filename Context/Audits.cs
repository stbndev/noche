using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Context
{
    public class Audits
    {
        [BsonRequired]
        public int sequence_value { get; set; }

        public int idcstatus { get; set; }

        [BsonRequired]
        [BsonDefaultValue(0)]
        public int idcompany { get; set; }
        [BsonRequired]
        [BsonDefaultValue("system_admin")]
        public string maker { get; set; }
        [BsonDefaultValue(0)]
        public int date_add { get; set; }
        [BsonDefaultValue(0)]
        public int date_set { get; set; }
    }
}
