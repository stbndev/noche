using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using noche.Models;
using noche.Services;

namespace noche
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<IDBSettings>(opt =>
            {
                //opt.ConnectionString = Configuration.GetConnectionString("DBSettings.ConnectionString");
                //opt.DatabaseName = Configuration.GetConnectionString("DBSettings.DatabaseName");
                opt.ConnectionString = Configuration.GetConnectionString("mongodb+srv://dbuser:develop3r@cluster0-pd5jd.gcp.mongodb.net/test?retryWrites=true&w=majority");
                opt.DatabaseName = Configuration.GetConnectionString("mrgvndb");
            });

            services.AddTransient<IProductRepository, ProductRepository>();

            //services.Configure<DBSettings>(Configuration.GetSection(nameof(DBSettings)));
            //services.AddSingleton<IDBSettings>(sp => sp.GetRequiredService<IOptions<DBSettings>>().Value);
            //services.AddSingleton<ProductsService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
