using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using noche.Context;

namespace noche.Data
{
    public class nocheContext : DbContext
    {
        public nocheContext (DbContextOptions<nocheContext> options)
            : base(options)
        {
        }

        public DbSet<noche.Context.Sales> Sales { get; set; }
    }
}
