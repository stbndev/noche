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
        [BsonDefaultValue("")]
        public string description { get; set; }

        [BsonRepresentation(BsonType.String)]
        [BsonDefaultValue("")]
        public string pathimg { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.Int32)]
        public int idcstatus { get; set; }

        

        private decimal _unitary_price;
        [BsonRepresentation(BsonType.Decimal128)]
        [BsonRequired]
        public decimal unitary_price
        {
            get { return _unitary_price; }
            set
            {
                _unitary_price = (value > 0) ? Util.Rounding2digits(value) : value;
            }
        }

        private decimal _unitary_cost;
        [BsonRepresentation(BsonType.Decimal128)]
        [BsonRequired]
        public decimal unitary_cost
        {
            get { return _unitary_cost; }
            set
            {
                _unitary_cost = (value > 0) ? Util.Rounding2digits(value) : value;
            }
        }


        private decimal _existence;
        [BsonRepresentation(BsonType.Decimal128)]
        [BsonRequired]
        public decimal existence
        {
            get { return _existence; }
            set
            {
                _existence = (value > 0) ? Util.Rounding2digits(value) : value;
            }
        }


    }
}
