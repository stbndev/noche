using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using noche.Config;
using noche.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace noche.Repository
{
    public interface IProductRepository
    {
        //Physical
        Task<bool> DeletePhysical(string id);
        Task<bool> Delete(string id);

        Task<Products> Update(Products values);
        Task<Products> Read(string id);
        Task<bool> Create(Products products);
        Task<IEnumerable<Products>> GetAll();
        //Task<bool> Create(Products products);

    }
    public class ProductsRepository : IProductRepository
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Mongosettings> _mongosettings;


        public ProductsRepository(IOptions<Mongosettings> settings)
        {
            _mongosettings = settings;
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<Products>> GetAll()
        {
            try
            {
                return await _context.Products.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Create(Products products)
        {
            try
            {
                int sequence_value = _context.ProductsNext();
                products.existence = 0;
                products.sequence_value = ++sequence_value;
                products.date_add = int.Parse(Util.ConvertToTimestamp());
                _context.Products.InsertOneAsync(products).Wait();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<Products> Read(string id)
        {
            try
            {
                int tmp = 0;
                int.TryParse(id, out tmp);

                if (tmp > 0)
                    return await _context.Products.Find(x => x.sequence_value == tmp).FirstAsync<Products>();
                else
                    return await _context.Products.Find(x => x.Id == id).FirstAsync<Products>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Products> Update(Products values)
        {
            try
            {
                FilterDefinition<Products> filter;
                int ds = int.Parse(Util.ConvertToTimestamp());
                var update = Builders<Products>.Update
                .Set(x => x.name, values.name)
                .Set(x => x.barcode, values.barcode)
                .Set(x => x.pathimg, values.pathimg)
                .Set(x => x.idcstatus, values.idcstatus)
                .Set(x => x.unitary_price, values.unitary_price)
                .Set(x => x.unitary_cost, values.unitary_cost)
                .Set(x => x.existence, values.existence)
                .Set(x => x.maker, values.maker)
                .Set(x => x.description, values.description)
                .Set(x => x.date_set, ds);
                //var result = await _fileRepository.UpdateOneAsync(fileId, update);

                if (!string.IsNullOrEmpty(values.Id))
                    filter = Builders<Products>.Filter.Eq(s => s.Id, values.Id);
                else
                    filter = Builders<Products>.Filter.Eq(s => s.sequence_value, values.sequence_value);

                await _context.Products.UpdateOneAsync(filter, update);

                var result = await Read(values.sequence_value.ToString());

                //var delete_result = await _context.Products.DeleteOneAsync(filter);
                //await _context.Products.InsertOneAsync(values);
                //var result = await _context.Products.ReplaceOneAsync(filter, values);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeletePhysical(string id)
        {
            try
            {
                FilterDefinition<Products> filter;
                int tmpid = 0;
                int.TryParse(id, out tmpid);

                if (tmpid >= 0)
                    filter = Builders<Products>.Filter.Eq(s => s.sequence_value, tmpid);
                else
                    filter = Builders<Products>.Filter.Eq(s => s.Id, id);

                var delete_result = await _context.Products.DeleteOneAsync(filter);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(string id)
        {
            int tmpid = 0;
            try
            {
                int ds = int.Parse(Util.ConvertToTimestamp());
                var update = Builders<Products>.Update
                .Set(x => x.idcstatus, (int)CSTATUS.ELIMINADO)
                .Set(x => x.date_set, ds);

                int.TryParse(id, out tmpid);

                FilterDefinition<Products> filter;
                if (tmpid >= 0)
                    filter = Builders<Products>.Filter.Eq(s => s.sequence_value, tmpid);
                else
                    filter = Builders<Products>.Filter.Eq(s => s.Id, id);

                await _context.Products.UpdateOneAsync(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
