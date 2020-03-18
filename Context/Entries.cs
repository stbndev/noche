using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using noche.Repository;

namespace noche.Context
{
    public class Entries : EntityBase
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.Int32)]
        public int identries { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.Int32)]
        public int idproducts { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal total { get; set; }

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

        private decimal _quantity;
        [BsonRepresentation(BsonType.Decimal128)]
        [BsonRequired]
        public decimal quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = (value > 0) ? Util.Rounding2digits(value) : value;
            }
        }
    }
}
