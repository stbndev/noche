using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noche.Context;
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
        public async Task<Products> Update() 
        {
            
        }

    }
}