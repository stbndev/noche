using Microsoft.Extensions.Options;
using MongoDB.Driver;
using noche.Config;
using noche.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace noche.Repository
{
    public interface IEntries
    {
        //Physical
        Task<bool> DeletePhysical(string id);
        Task<bool> Delete(string id);

        Task<Entries> Update(Entries values);
        Task<Entries> Read(string id);
        Task<bool> Create(Entries products);
        Task<IEnumerable<Entries>> GetAll();

    }
    public class EntriesRepository : IEntries
    {
        private readonly IProductRepository _productRepository = null;
        private readonly MongoContext _context = null;
        private readonly IOptions<Nochesettings> _mongosettings;


        public EntriesRepository(IOptions<Nochesettings> settings)
        {
            _mongosettings = settings;
            _context = new MongoContext(settings);
            _productRepository = new ProductsRepository(settings);
        }

        public async Task<bool> Create(Entries values)
        {
            try
            {
                var product = await _productRepository.Read(values.idproducts.ToString());

                int sequence_value = _context.EntriesNext();
                values.identries = ++sequence_value;
                values.date_add = int.Parse(Util.ConvertToTimestamp());
                values.date_set = 0;
                values.total = values.unitary_cost * values.quantity;
                values.unitary_price = product.unitary_price;

                product.unitary_cost = values.unitary_cost;
                product.existence = product.existence + values.quantity;
                product.maker = values.maker;

                values.existence = product.existence;

                _context.Entries.InsertOneAsync(values).Wait();
                await _productRepository.Update(product);
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
                var entries = await this.Read(id);
                var product = await _productRepository.Read(entries.idproducts.ToString());

                var update = Builders<Entries>.Update
                .Set(x => x.idcstatus, (int)CSTATUS.ELIMINADO)
                .Set(x => x.date_set, ds);

                int.TryParse(id, out tmpid);

                FilterDefinition<Entries> filter;
                if (tmpid >= 0)
                    filter = Builders<Entries>.Filter.Eq(s => s.identries, tmpid);
                else
                    filter = Builders<Entries>.Filter.Eq(s => s.Id, id);

                product.existence = product.existence - entries.quantity;
                await _productRepository.Update(product);
                await _context.Entries.UpdateOneAsync(filter, update);
                return true;
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
                var entries = await this.Read(id);
                var product = await _productRepository.Read(entries.idproducts.ToString());
                FilterDefinition<Entries> filter;
                int tmpid = 0;
                int.TryParse(id, out tmpid);

                if (tmpid >= 0)
                    filter = Builders<Entries>.Filter.Eq(s => s.identries, tmpid);
                else
                    filter = Builders<Entries>.Filter.Eq(s => s.Id, id);

                if (entries.idcstatus != (int)CSTATUS.ELIMINADO)
                    product.existence = product.existence - entries.quantity;

                await _productRepository.Update(product);
                var delete_result = await _context.Entries.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Entries>> GetAll()
        {
            try
            {
                return await _context.Entries.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Entries> Read(string id)
        {
            try
            {
                int tmp = 0;
                int.TryParse(id, out tmp);

                if (tmp > 0)
                    return await _context.Entries.Find(x => x.identries == tmp).FirstAsync<Entries>();
                else
                    return await _context.Entries.Find(x => x.Id == id).FirstAsync<Entries>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Entries> Update(Entries values)
        {
            try
            {
                var product = await _productRepository.Read(values.idproducts.ToString());
                var entries = await this.Read(values.identries.ToString());

                FilterDefinition<Entries> filter = Builders<Entries>.Filter.Eq(s => s.identries, values.identries);

                int ds = int.Parse(Util.ConvertToTimestamp());
                values.total = values.unitary_cost * values.quantity;
                
                product.unitary_cost = values.unitary_cost;
                // product.unitary_price = values.unitary_price;
                product.existence = product.existence - entries.quantity;
                product.existence = product.existence + values.quantity;
                product.maker = values.maker;
                values.existence = product.existence;

                var update = Builders<Entries>.Update
                .Set(x => x.total, values.total)
                .Set(x => x.date_set, ds)
                .Set(x => x.idcstatus, values.idcstatus)
                .Set(x => x.idcompany, values.idcompany)
                .Set(x => x.unitary_cost, values.unitary_cost)
                // .Set(x => x.unitary_cost, values.unitary_cost)
                .Set(x => x.existence, values.existence)
                .Set(x => x.quantity, values.quantity);

                await _productRepository.Update(product);
                await _context.Entries.UpdateOneAsync(filter, update);

                var result = await Read(values.identries.ToString());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
