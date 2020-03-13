﻿using Microsoft.AspNetCore.Mvc;
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
    public class ShrinkageController : ControllerBase
    {
        private readonly IShrinkage _repository;

        public ShrinkageController(IShrinkage ish) { _repository = ish; }

        [HttpGet]
        //public async Task<IEnumerable<Products>> Get()
        public async Task<ResponseModel> Get()
        {
            return await executeactionAsync(Action.READALL);
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel> Read(string id)
        {
            return await executeactionAsync(Action.READID, id: id);
        }

        [HttpPost]
        public async Task<ResponseModel> Create(Shrinkages values)
        {
            return await executeactionAsync(Action.CREATE, values: values);
        }

        [HttpPut("{id}")]
        public async Task<ResponseModel> Update(string id, Shrinkages values)
        {
            return await executeactionAsync(Action.UPDATE, values: values);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(string id)
        {
            return await executeactionAsync(Action.DELETE, id: id);
        }

        [HttpDelete]
        [Route("{id}/physical")]
        public async Task<ResponseModel> DeletePhysical(string id)
        {
            return await executeactionAsync(Action.DELETEPHYSICAL, id: id);
        }
        private async Task<ResponseModel> executeactionAsync(Action action, string id = "", Shrinkages values = null)
        {
            ResponseModel rm = new ResponseModel();
            Shrinkages result = new Shrinkages();

            try
            {
                switch (action)
                {
                    case Action.CREATE:
                        rm.response = await _repository.Create(values);
                        rm.result = values;
                        rm.SetResponse(rm.response, string.Empty);
                        break;

                    case Action.READID:
                        rm.result = await _repository.Read(id);
                        if (!string.IsNullOrEmpty(rm.result.Id))
                            rm.SetResponse(true, string.Empty);
                        break;

                    case Action.READALL:
                        var list = await _repository.GetAll();
                        rm.response = list.Count() > 0 ? true : false;
                        rm.result = list;
                        rm.SetResponse(rm.response, string.Empty);
                        break;

                    case Action.UPDATE:
                        rm.result = await _repository.Update(values);
                        if (!string.IsNullOrEmpty(rm.result.Id))
                            rm.SetResponse(true, string.Empty);
                        break;

                    case Action.DELETE:
                        rm.response = await _repository.Delete(id);
                        rm.SetResponse(rm.response, string.Empty);
                        break;

                    case Action.DELETEPHYSICAL:
                        rm.response = await _repository.DeletePhysical(id);
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