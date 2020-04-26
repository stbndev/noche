using Microsoft.Extensions.Options;
using MongoDB.Driver;
using noche.Config;
using noche.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace noche.Repository
{
    public interface IUsers
    {
        Task<bool> Delete(string id);
        Task<Users> Update(Users values);
        Task<Users> Read(string id);
        Task<bool> Create(Users values);
        Task<IEnumerable<Users>> GetAll();
    }


    public interface IRol_Operation
    {
        Task<bool> Delete(string id);
        Task<Rol_Operation> Update(Rol_Operation values);
        Task<Rol_Operation> Read(string id);
        Task<bool> Create(Rol_Operation values);
        Task<IEnumerable<Rol_Operation>> GetAll();
    }

    public class RolOperationsRepository : IRol_Operation
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Nochesettings> _mongosettings;
        public async Task<bool> Create(Rol_Operation values)
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

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Rol_Operation>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Rol_Operation> Read(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Rol_Operation> Update(Rol_Operation values)
        {
            throw new NotImplementedException();
        }
    }

    public class UsersRepository : IUsers
    {
        private readonly MongoContext _context = null;
        private readonly IOptions<Nochesettings> _mongosettings;
        // private readonly IProductRepository _productRepository = null;

        public UsersRepository(IOptions<Nochesettings> settings)
        {
            _mongosettings = settings;
            _context = new MongoContext(settings);

        }
        public async Task<bool> Create(Users values)
        {
            try
            {
                values.date_add = int.Parse(Util.ConvertToTimestamp());
                _context.Users.InsertOneAsync(values).Wait();
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
                FilterDefinition<Users> filter = Builders<Users>.Filter.Eq(s => s.Id, id);
                var delete_result = await _context.Users.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            try
            {
                return await _context.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Users> Read(string id)
        {
            try
            {
                return await _context.Users.Find(x => x.Id == id).FirstAsync<Users>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Users> Update(Users values)
        {
            try
            {
                var update = Builders<Users>.Update
                .Set(x => x.name, values.name)
                .Set(x => x.email, values.email)
                .Set(x => x.password, values.password)
                .Set(x => x.idrol, values.idrol);

                FilterDefinition<Users> filter = Builders<Users>.Filter.Eq(s => s.Id, values.Id);
                await _context.Users.UpdateOneAsync(filter, update);
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
