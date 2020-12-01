using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using noche.Context;
using noche.Models;
using noche.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesOperationsController : ControllerBase
    {
        private readonly IRolesOperations _repository;

        public RolesOperationsController(IRolesOperations ntrfc) { _repository = ntrfc; }

        [HttpPost]
        public async Task<ResponseNocheServices> CreateRO(Rol_Operation values)
        {
            return await executeactionAsyncRO(Action.CREATE, values: values);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseNocheServices> ReadRO(string id)
        {
            return await executeactionAsyncRO(Action.READID, id: id);
        }

        [HttpGet]
        public async Task<ResponseNocheServices> ReadAllRO()
        {
            return await executeactionAsyncRO(Action.READALL);
        }

        [HttpPut]
        public async Task<ResponseNocheServices> UpdateRO(Rol_Operation values)
        {
            return await executeactionAsyncRO(Action.UPDATE, values: values);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseNocheServices> DeleteRO(string id)
        {
            return await executeactionAsyncRO(Action.DELETE, id: id);
        }

        private async Task<ResponseNocheServices> executeactionAsyncRO(Action action, string id = "", Rol_Operation values = null)
        {
            ResponseNocheServices rm = new ResponseNocheServices();
            var result = new Rol_Operation();

            try
            {
                switch (action)
                {
                    case Action.CREATE:
                        rm.Flag = await _repository.CreateRO(values);
                        rm.Data = values;
                        rm.SetResponse(rm.Flag, string.Empty);
                        break;

                    case Action.READID:
                        rm.Data = await _repository.ReadRO(id);
                        if (!string.IsNullOrEmpty(rm.Data.Id))
                            rm.SetResponse(1, string.Empty);
                        break;

                    case Action.READALL:
                        var list = await _repository.GetAllRO();
                        rm.Flag = list.Count() > 0 ? 1: 0;
                        rm.Data = list;
                        rm.SetResponse(rm.Flag, string.Empty);
                        break;

                    case Action.UPDATE:
                        values.Id = id;
                        rm.Data = await _repository.UpdateRO(values);
                        if (!string.IsNullOrEmpty(rm.Data.Id))
                            rm.SetResponse(1, string.Empty);
                        break;

                    case Action.DELETE:
                        rm.Flag = await _repository.DeleteRO(id);
                        rm.SetResponse(rm.Flag, string.Empty);
                        break;

                    case Action.DELETEPHYSICAL:
                        //rm.Flag = await _repository.DeletePhysical(id);
                        rm.SetResponse(rm.Flag, string.Empty);
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                rm.Message = ex.Message;
            }
            return rm;
        }

    }
}