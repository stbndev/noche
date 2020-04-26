using Microsoft.Extensions.Options;
using MongoDB.Driver;
using noche.Config;
using noche.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace noche.Repository
{
    public interface IRolesOperations
    {
        Task<bool> DeleteRO(string id);
        Task<Rol_Operation> UpdateRO(Rol_Operation values);
        Task<Rol_Operation> ReadRO(string id);
        Task<IEnumerable<Rol_Operation>> GetAllRO();
        Task<bool> CreateRO(Rol_Operation values);
    }
    public class RolesOperationsRepository : IRolesOperations
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Nochesettings> _mongosettings;

        public RolesOperationsRepository(IOptions<Nochesettings> settings)
        {
            _mongosettings = settings;
            _context = new MongoContext(settings);
        }

        public async Task<bool> CreateRO(Rol_Operation values)
        {
            try
            {
                _context.Rol_Operation.InsertOneAsync(values).Wait();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteRO(string id)
        {
            try
            {
                FilterDefinition<Rol_Operation> filter = Builders<Rol_Operation>.Filter.Eq(s => s.Id, id);
                var delete_result = await _context.Rol_Operation.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<Rol_Operation>> GetAllRO()
        {
            try
            {
                return await _context.Rol_Operation.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Rol_Operation> ReadRO(string id)
        {
            try
            {
                return await _context.Rol_Operation.Find(x => x.Id == id).FirstAsync<Rol_Operation>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Rol_Operation> UpdateRO(Rol_Operation values)
        {
            try
            {
                var update = Builders<Rol_Operation>.Update
                .Set(x => x.idrol, values.idrol)
                .Set(x => x.idoperation, values.idoperation);

                FilterDefinition<Rol_Operation> filter = Builders<Rol_Operation>.Filter.Eq(s => s.Id, values.Id);
                await _context.Rol_Operation.UpdateOneAsync(filter, update);
                var result = await ReadRO(values.Id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
