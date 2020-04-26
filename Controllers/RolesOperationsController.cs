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
        public async Task<ResponseModel> CreateRO(Rol_Operation values)
        {
            return await executeactionAsyncRO(Action.CREATE, values: values);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseModel> ReadRO(string id)
        {
            return await executeactionAsyncRO(Action.READID, id: id);
        }

        [HttpGet]
        public async Task<ResponseModel> ReadAllRO()
        {
            return await executeactionAsyncRO(Action.READALL);
        }

        [HttpPut]
        public async Task<ResponseModel> UpdateRO(Rol_Operation values)
        {
            return await executeactionAsyncRO(Action.UPDATE, values: values);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseModel> DeleteRO(string id)
        {
            return await executeactionAsyncRO(Action.DELETE, id: id);
        }

        private async Task<ResponseModel> executeactionAsyncRO(Action action, string id = "", Rol_Operation values = null)
        {
            ResponseModel rm = new ResponseModel();
            var result = new Rol_Operation();

            try
            {
                switch (action)
                {
                    case Action.CREATE:
                        rm.response = await _repository.CreateRO(values);
                        rm.result = values;
                        rm.SetResponse(rm.response, string.Empty);
                        break;

                    case Action.READID:
                        rm.result = await _repository.ReadRO(id);
                        if (!string.IsNullOrEmpty(rm.result.Id))
                            rm.SetResponse(true, string.Empty);
                        break;

                    case Action.READALL:
                        var list = await _repository.GetAllRO();
                        rm.response = list.Count() > 0 ? true : false;
                        rm.result = list;
                        rm.SetResponse(rm.response, string.Empty);
                        break;

                    case Action.UPDATE:
                        values.Id = id;
                        rm.result = await _repository.UpdateRO(values);
                        if (!string.IsNullOrEmpty(rm.result.Id))
                            rm.SetResponse(true, string.Empty);
                        break;

                    case Action.DELETE:
                        rm.response = await _repository.DeleteRO(id);
                        rm.SetResponse(rm.response, string.Empty);
                        break;

                    case Action.DELETEPHYSICAL:
                        //rm.response = await _repository.DeletePhysical(id);
                        rm.SetResponse(rm.response, string.Empty);
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                rm.message = ex.Message;
            }
            return rm;
        }

    }
}