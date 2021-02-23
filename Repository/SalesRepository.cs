using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using noche.Context;
using noche.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Threading;
using System.ComponentModel.DataAnnotations;

namespace noche.Repository
{
    public interface ISales
    {
        //Physical
        Task<int> DeletePhysical(string id);
        Task<int> Delete(string id);

        Task<Sales> Update(Sales values);
        Task<Sales> Read(string id);
        Task<int> Create(Sales values);
        Task<IEnumerable<Sales>> GetAll();

    }
    public class SalesRepository : ISales
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Nochesettings> _mongosettings;
        private readonly IProductRepository _productRepository = null;

        public SalesRepository(IOptions<Nochesettings> settings)
        {
            _mongosettings = settings;
            _context = new MongoContext(settings);
            _productRepository = new ProductsRepository(settings);
        }

        public async Task<int> Create(Sales values)
        {
            string id = string.Empty;
            using (var session = _context.client.StartSession())
            {
                session.StartTransaction();
                try
                {
                    int sequence_value = _context.SalesNext();
                    values.idsales = ++sequence_value;
                    values.idcstatus = (int)CSTATUS.ACTIVO;
                    values.date_add = int.Parse(Util.ConvertToTimestamp());
                    values.total = values.details.Sum(x => x.quantity * x.unitary_price);

                    foreach (var item in values.details)
                    {
                        var product = await _productRepository.Read(item.idproducts.ToString());
                        product.existence = product.existence - item.quantity;
                        if (product.existence <= 0)
                            throw new Exception(String.Format("* Producto sin inventario; detalles: {0}", product.name));

                        values.total = values.details.Sum(x => x.quantity * item.unitary_price);

                        item.unitary_cost = product.unitary_cost;
                        await _productRepository.Update(product);
                    }
                    _context.Sales.InsertOneAsync(values).Wait();
                    id = values.Id;
                    session.CommitTransaction();
                    return 1;
                }
                catch (Exception ex)
                {
                    session.AbortTransaction();
                    return -1;
                    throw new Exception(" *Error  al momento de incertar " + ex.Message);

                }
            }
        }

        public async Task<int> Delete(string id)
        {
            int tmpid = 0;
            try
            {
                int ds = int.Parse(Util.ConvertToTimestamp());
                var update = Builders<Sales>.Update
                .Set(x => x.idcstatus, (int)CSTATUS.ELIMINADO)
                .Set(x => x.date_set, ds);

                int.TryParse(id, out tmpid);

                FilterDefinition<Sales> filter;
                filter = (tmpid > 0) ? Builders<Sales>.Filter.Eq(s => s.idsales, tmpid) : Builders<Sales>.Filter.Eq(s => s.Id, id);

                await _context.Sales.UpdateOneAsync(filter, update);

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeletePhysical(string id)
        {
            try
            {
                FilterDefinition<Sales> filter;
                int tmpid = 0;
                int.TryParse(id, out tmpid);

                if (tmpid >= 0)
                    filter = Builders<Sales>.Filter.Eq(s => s.idsales, tmpid);
                else
                    filter = Builders<Sales>.Filter.Eq(s => s.Id, id);

                var delete_result = await _context.Sales.DeleteOneAsync(filter);

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Sales>> GetAll()
        {
            try
            {
                return await _context.Sales.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sales> Read(string id)
        {
            try
            {
                int tmp = 0;
                int.TryParse(id, out tmp);

                if (tmp > 0)
                    return await _context.Sales.Find(x => x.idsales == tmp).FirstAsync<Sales>();
                else
                    return await _context.Sales.Find(x => x.Id == id).FirstAsync<Sales>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sales> Update(Sales values)
        {
            try
            {
                if (values.idcstatus != (int)CSTATUS.CANCELADO)
                    throw new Exception("* Operación invalida");
                else
                {
                    var sales = await Read(values.idsales.ToString());
                    foreach (var itemdb in sales.details)
                    {
                        var product = await _productRepository.Read(itemdb.idproducts.ToString());
                        product.existence = product.existence + itemdb.quantity;
                        await _productRepository.Update(product);
                    }
                }


                int ds = int.Parse(Util.ConvertToTimestamp());
                var update = Builders<Sales>.Update
                .Set(x => x.date_set, ds)
                .Set(x => x.maker, values.maker)
                .Set(x => x.idcstatus, values.idcstatus);

                int tmpid = 0;
                int.TryParse(values.Id, out tmpid);
                FilterDefinition<Sales> filter = (tmpid > 0) ? Builders<Sales>.Filter.Eq(s => s.idsales, values.idsales) : Builders<Sales>.Filter.Eq(s => s.Id, values.Id);

                await _context.Sales.UpdateOneAsync(filter, update);
                var result = await Read(values.idsales.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
