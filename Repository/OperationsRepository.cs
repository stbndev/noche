using Microsoft.Extensions.Options;
using MongoDB.Driver;
using noche.Config;
using noche.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace noche.Repository
{
    public interface IOperations
    {
        Task<bool> Delete(string id);
        Task<Operations> Update(Operations values);
        Task<Operations> Read(string id);
        Task<bool> Create(Operations values);
        Task<IEnumerable<Operations>> GetAll();
    }

    public class OperationsRepository : IOperations
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Nochesettings> _mongosettings;

        public OperationsRepository(IOptions<Nochesettings> settings)
        {
            _mongosettings = settings;
            _context = new MongoContext(settings);
        }

        public async Task<bool> Create(Operations values)
        {
            try
            {
                _context.Operations.InsertOneAsync(values).Wait();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                int tmpid = 0;
                int.TryParse(id, out tmpid);
                FilterDefinition<Operations> filter = Builders<Operations>.Filter.Eq(s => s.Id, id);
                var delete_result = await _context.Operations.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Operations>> GetAll()
        {
            try
            {
                return await _context.Operations.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Operations> Read(string id)
        {
            try
            {
                return await _context.Operations.Find(x => x.Id == id).FirstAsync<Operations>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Operations> Update(Operations values)
        {
            try
            {
                int ds = int.Parse(Util.ConvertToTimestamp());
                var update = Builders<Operations>.Update
                .Set(x => x.name, values.name)
                .Set(x => x.idmodules, values.idmodules);

                FilterDefinition<Operations> filter = Builders<Operations>.Filter.Eq(s => s.Id, values.Id);
                await _context.Operations.UpdateOneAsync(filter, update);
                var result = await Read(values.Id);
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
