using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using noche.Context;
using noche.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

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
        private readonly IOptions<Mongosettings> _mongosettings;


        public EntriesRepository(IOptions<Mongosettings> settings)
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
                values.sequence_value = ++sequence_value;
                values.date_add = int.Parse(Util.ConvertToTimestamp());
                values.total = values.unitary_cost * values.quantity;
                _context.Entries.InsertOneAsync(values).Wait();

                product.unitary_cost = values.unitary_cost;
                product.existence = product.existence + values.quantity;
                product.maker = values.maker;

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
                var update = Builders<Entries>.Update
                .Set(x => x.idcstatus, (int)CSTATUS.ELIMINADO)
                .Set(x => x.date_set, ds);

                int.TryParse(id, out tmpid);

                FilterDefinition<Entries> filter;
                if (tmpid >= 0)
                    filter = Builders<Entries>.Filter.Eq(s => s.sequence_value, tmpid);
                else
                    filter = Builders<Entries>.Filter.Eq(s => s.Id, id);

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
                FilterDefinition<Entries> filter;
                int tmpid = 0;
                int.TryParse(id, out tmpid);

                if (tmpid >= 0)
                    filter = Builders<Entries>.Filter.Eq(s => s.sequence_value, tmpid);
                else
                    filter = Builders<Entries>.Filter.Eq(s => s.Id, id);

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
                    return await _context.Entries.Find(x => x.sequence_value == tmp).FirstAsync<Entries>();
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
                FilterDefinition<Entries> filter;
                int ds = int.Parse(Util.ConvertToTimestamp());
                values.total = values.unitary_cost * values.quantity;

                var update = Builders<Entries>.Update
                .Set(x => x.total,values.total )
                .Set(x => x.date_set, ds)
                .Set(x => x.idcstatus, values.idcstatus)
                .Set(x => x.unitary_cost, values.unitary_cost)
                .Set(x => x.quantity, values.quantity);



                //var result = await _fileRepository.UpdateOneAsync(fileId, update);

                if (!string.IsNullOrEmpty(values.Id))
                    filter = Builders<Entries>.Filter.Eq(s => s.Id, values.Id);
                else
                    filter = Builders<Entries>.Filter.Eq(s => s.sequence_value, values.sequence_value);

                await _context.Entries.UpdateOneAsync(filter, update);
                var result = await Read(values.sequence_value.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
