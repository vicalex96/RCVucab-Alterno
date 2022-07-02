using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using proveedor.Persistence.DAOs;
using proveedor.Persistence.DAOs.Implementations;
using proveedor.Persistence.DAOs.Interfaces;
using proveedor.Persistence.Database;

namespace proveedor
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

        services.AddControllers();
            services.AddDbContext<ProveedorDbContext>(options =>
            options.UseNpgsql(Configuration["DBConnectionString"], x => x.UseNetTopologySuite()));

            services.AddTransient<IProveedorDbContext, ProveedorDbContext>();
            //services.AddTransient<IProviderDAO, ProviderDAO>();
            services.AddTransient<ICotizacionParteDAO, CotizacionParteDAO>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "proveedor", Version = "v1" });
            });
            services.AddRouting(routing => routing.LowercaseUrls = true);

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
