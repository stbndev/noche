﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using noche.Repository;
using System.Collections.Generic;

namespace noche.Context
{
    public class Shrinkages : EntityBase
    {
        [BsonRequired]
        [BsonDefaultValue(0)]
        public int idshrinkages { get; set; }

        [BsonRequired]
        [BsonDefaultValue(0)]
        public decimal total { get; set; }

        public List<ShrinkageDetails> details { get; set; }
    }

    public class ShrinkageDetails
    {
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
        [BsonRequired]
        public int idproducts { get; set; }

    }
}
