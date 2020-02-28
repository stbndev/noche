using noche.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace noche.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAll();
    }
    public class ProductsRepository
    {
    }
}
