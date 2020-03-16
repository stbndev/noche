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
    public interface IShrinkage
    {
        Task<bool> DeletePhysical(string id);
        Task<bool> Delete(string id);

        Task<Shrinkages> Update(Shrinkages values);
        Task<Shrinkages> Read(string id);
        Task<bool> Create(Shrinkages products);
        Task<IEnumerable<Shrinkages>> GetAll();
    }
    public class ShrinkagesRepository : IShrinkage
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Nochesettings> _mongosettings;
        private readonly IProductRepository _productRepository = null;

        public ShrinkagesRepository(IOptions<Nochesettings> settings)
        {
            _mongosettings = settings;
            _context = new MongoContext(settings);
            _productRepository = new ProductsRepository(settings);
        }
        public async Task<bool> Create(Shrinkages values)
        {
            decimal total = 0;
            try
            {                
                int sequence_value = _context.ShrinkageNext();
                values.sequence_value = ++sequence_value;
                values.date_add = int.Parse(Util.ConvertToTimestamp());

                foreach (var item in values.details)
                {
                    var product = await _productRepository.Read(item.idproducts.ToString());
                    var subtotal = product.unitary_cost * item.quantity;
                    item.unitary_cost = product.unitary_cost;
                    total += subtotal ;
                }
                values.total = total;
                _context.Shrinkages.InsertOneAsync(values).Wait();
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
                var update = Builders<Shrinkages>.Update
                .Set(x => x.idcstatus, (int)CSTATUS.ELIMINADO)
                .Set(x => x.date_set, ds);

                int.TryParse(id, out tmpid);
                FilterDefinition<Shrinkages> filter;

                if (tmpid >= 0)
                    filter = Builders<Shrinkages>.Filter.Eq(s => s.sequence_value, tmpid);
                else
                    filter = Builders<Shrinkages>.Filter.Eq(s => s.Id, id);

                await _context.Shrinkages.UpdateOneAsync(filter, update);
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
                FilterDefinition<Shrinkages> filter;
                int tmpid = 0;
                int.TryParse(id, out tmpid);

                if (tmpid >= 0)
                    filter = Builders<Shrinkages>.Filter.Eq(s => s.sequence_value, tmpid);
                else
                    filter = Builders<Shrinkages>.Filter.Eq(s => s.Id, id);

                var delete_result = await _context.Shrinkages.DeleteOneAsync(filter);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Shrinkages>> GetAll()
        {
            try
            {
                return await _context.Shrinkages.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Shrinkages> Read(string id)
        {
            try
            {
                int tmp = 0;
                int.TryParse(id, out tmp);

                if (tmp > 0)
                    return await _context.Shrinkages.Find(x => x.sequence_value == tmp).FirstAsync<Shrinkages>();
                else
                    return await _context.Shrinkages.Find(x => x.Id == id).FirstAsync<Shrinkages>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Shrinkages> Update(Shrinkages values)
        {
            try
            {
                FilterDefinition<Shrinkages> filter;
                int ds = int.Parse(Util.ConvertToTimestamp());
                var update = Builders<Shrinkages>.Update
                .Set(x => x.total, values.total)
                .Set(x => x.date_set, ds)
                .Set(x => x.idcstatus, values.idcstatus)
                .Set(x => x.maker, values.maker)
                .Set(x => x.details, values.details);

                //var result = await _fileRepository.UpdateOneAsync(fileId, update);

                if (!string.IsNullOrEmpty(values.Id))
                    filter = Builders<Shrinkages>.Filter.Eq(s => s.Id, values.Id);
                else
                    filter = Builders<Shrinkages>.Filter.Eq(s => s.sequence_value, values.sequence_value);

                await _context.Shrinkages.UpdateOneAsync(filter, update);
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
