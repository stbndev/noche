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
    public interface ISales
    {
        //Physical
        Task<bool> DeletePhysical(string id);
        Task<bool> Delete(string id);

        Task<Sales> Update(Sales values);
        Task<Sales> Read(string id);
        Task<bool> Create(Sales values);
        Task<IEnumerable<Sales>> GetAll();

    }
    public class SalesRepository : ISales
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Mongosettings> _mongosettings;

        public SalesRepository(IOptions<Mongosettings> settings)
        {
            _mongosettings = settings;
            _context = new MongoContext(settings);
        }

        public async  Task<bool> Create(Sales values)
        {
            try
            {
                int sequence_value = _context.SalesNext();
                values.sequence_value = ++sequence_value;
                values.date_add = int.Parse(Util.ConvertToTimestamp());
                _context.Sales.InsertOneAsync(values).Wait();
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
                var update = Builders<Sales>.Update
                .Set(x => x.idcstatus, (int)CSTATUS.ELIMINADO)
                .Set(x => x.date_set, ds);

                int.TryParse(id, out tmpid);

                FilterDefinition<Sales> filter;
                if (tmpid >= 0)
                    filter = Builders<Sales>.Filter.Eq(s => s.sequence_value, tmpid);
                else
                    filter = Builders<Sales>.Filter.Eq(s => s.Id, id);

                await _context.Sales.UpdateOneAsync(filter, update);
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
                FilterDefinition<Sales> filter;
                int tmpid = 0;
                int.TryParse(id, out tmpid);

                if (tmpid >= 0)
                    filter = Builders<Sales>.Filter.Eq(s => s.sequence_value, tmpid);
                else
                    filter = Builders<Sales>.Filter.Eq(s => s.Id, id);

                var delete_result = await _context.Sales.DeleteOneAsync(filter);

                return true;
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
                    return await _context.Sales.Find(x => x.sequence_value == tmp).FirstAsync<Sales>();
                else
                    return await _context.Sales.Find(x => x.Id == id).FirstAsync < Sales>();
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
                FilterDefinition<Entries> filter;
                int ds = int.Parse(Util.ConvertToTimestamp());
                var update = Builders<Entries>.Update
                .Set(x => x.total, values.total)
                .Set(x => x.date_set, ds)
                .Set(x => x.idcstatus, values.idcstatus);


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
