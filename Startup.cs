using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using noche.Config;
using noche.Repository;
using Microsoft.EntityFrameworkCore;
using noche.Data;

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
            //services.AddMvc();
            //start
            // Add functionality to inject IOptions<T>
            services.Configure<Mongosettings>(Configuration.GetSection("Mongosettings"));
            services.AddTransient<IProductRepository, ProductsRepository>();
            services.AddTransient<ICstatus, CstatusRepository>();
            // end
            services.AddControllers();

            //services.AddDbContext<nocheContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("nocheContext")));
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
