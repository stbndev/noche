﻿using MongoDB.Bson;
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
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }
        
        [BsonRequired]
        public string name { get; set; }
        [BsonRequired]
        public string barcode { get; set; }
        
        public string pathimg { get; set; }

        [BsonRequired]
        public int idcstatus { get; set; }

        public decimal unitary_price { get; set; }

        public decimal unitary_cost { get; set; }
        public decimal existence { get; set; }

        
        /*
         * SELECT [id]
      ,[name]
      ,[barcode]
      ,[idcstatus]
      ,[unitary_price]
      ,[unitary_cost]
      ,[existence]
      ,[pathimg]
  FROM [dbo].[PRODUCTS]
GO
         */

    }
}
