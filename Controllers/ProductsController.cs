using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noche.Context;
using noche.Models;
using noche.Repository;

namespace noche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly ProductsService _productService;
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Products>> Get()
        {
            return await _repository.GetAll();
        }

        //[HttpGet("{id}",Name = "ProductsGet")]
             [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<Products> Read(int id)
        {
            return await _repository.Read(id);
        }
 
        [HttpPost]
        public async Task<ResponseModel> Create(Products products)
        {
            return await executeactionAsync(Action.CREATE, value: products);
        }
        private async Task<ResponseModel> executeactionAsync(Action action, int id = 0, Products value = null)
        {
            ResponseModel rm = new ResponseModel();
            Products result = new Products();

            try
            {

                switch (action)
                {
                    case Action.CREATE:
                        rm.response = await _repository.Create(value);
                        break;
                    case Action.READID:
                        //result = ng.Read(id: id).First();
                        break;
                    case Action.READALL:
                        //var results = ng.Read(all: true);
                        //// rm.result = results;
                        //List<ProductDTO> p = Mapper.Map<List<ProductDTO>>(results);
                        //rm.result = p;
                        //rm.SetResponse(true);

                        break;
                    case Action.UPDATE:
                        //value.idproducts = id > 0 ? id : value.idproducts;
                        //result = ng.Update(value);
                        //rm.SetResponse(true);
                        break;
                    case Action.DELETE:
                        //bool flag = ng.Delete(id);
                        break;

                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(result.Id))
                {
                    //ProductDTO p = Mapper.Map<ProductDTO>(result);
                    rm.result = value;
                    rm.SetResponse(true);
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