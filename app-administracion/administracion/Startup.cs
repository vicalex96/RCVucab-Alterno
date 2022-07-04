using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.Conections.rabbit;

namespace administracion
{
    public class Startup
    {
        private readonly string  _MyCors ="Mycors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<AdminDBContext>( 
                o => o.UseNpgsql(Configuration.GetConnectionString("cnDatabase"))
                );
                
            services.AddTransient<IAdminDBContext, AdminDBContext>();
            services.AddTransient<IAseguradoDAO, AseguradoDAO>();
            services.AddTransient<IVehiculoDAO, VehiculoDAO>();
            services.AddTransient<IPolizaDAO, PolizaDAO>();
            services.AddTransient<IIncidenteDAO, IncidenteDAO>();
            services.AddTransient<ITallerDAO, TallerDAO>();
            services.AddTransient<IProveedorDAO, ProveedorDAO>();
            services.AddTransient<IProductorRabbit, ProductorRabbit>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "administracion", Version = "v1" });
            });
            services.AddRouting(routing => routing.LowercaseUrls = true);

            services.AddCors(options =>
            {
                options.AddPolicy(name: _MyCors, builder => 
                {
                    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost").AllowAnyHeader().AllowAnyMethod();
                });
            });
            

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

            app.UseSwagger();
            app.UseSwaggerUI( c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","My API V1");
                c.RoutePrefix = "";
            });
        }
    }
}