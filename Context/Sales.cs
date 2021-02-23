using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using noche.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace noche.Context
{
    public class Sales : EntityBase
    {
        [BsonRequired]
        [BsonRepresentation(BsonType.Int32)]
        //[Range(1, double.MaxValue, ErrorMessage = " * Id de venta es necesario")]
        public int idsales { get; set; }

        [BsonRequired]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id de usuario es necesario")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [UsersExists(ErrorMessage =" * Verificar Cliente")]
        public  string  idusers{ get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal total { get; set; }

        [MinLength(1,ErrorMessage ="Debe existir detalles de venta")]
        public List<SalesDetails> details { get; set; }

    }

    public class SalesDetails
    {
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
