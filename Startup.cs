using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using noche.Config;
using noche.Repository;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace noche
{
    public class Startup
    {
        readonly string MyCors = "MyCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // start jtw
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x2 =>
            {
                x2.RequireHttpsMetadata = false;
                x2.SaveToken = true;
                x2.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            // end jwt

            //start
           
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyCors,
                                   builder =>
                                   {
                                       builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                                   });
            });

            services.Configure<Nochesettings>(Configuration.GetSection("Nochesettings"));
            services.AddScoped<ISales, SalesRepository>();
            services.AddTransient<IRolesOperations, RolesOperationsRepository>();
            services.AddTransient<IRoles, RolesRepository>();
            services.AddTransient<IModules, ModulesRepository>();
            services.AddTransient<IOperations, OperationsRepository>();
            services.AddTransient<IUsers, UsersRepository>();
            // services.AddTransient<IUsers, UserExitsAttribute>();

            services.AddTransient<IProductRepository, ProductsRepository>();
            services.AddTransient<IEntries, EntriesRepository>();
            services.AddTransient<ICstatus, CstatusRepository>();
           services.AddTransient<IShrinkage, ShrinkagesRepository>();
            services.AddTransient<IDocFile, DocFileRepository>();
            // end
            services.AddControllers();
            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyCors);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
            });
            // jwt start
            //app.UseMvc();
            // jwt end
        }
    }
}
