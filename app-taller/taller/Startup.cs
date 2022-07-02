using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using taller.Persistence.Database;
using taller.Persistence.DAOs;

namespace taller
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
            services.AddDbContext<TallerDBContext>( 
                o => o.UseNpgsql(Configuration.GetConnectionString("cnDatabase"))
                );

            services.AddTransient<ITallerDBContext, TallerDBContext>();
            services.AddTransient<ITallerDAO, TallerDAO>();
            services.AddTransient<ICotizacionRepDAO, CotizacionRepDAO>();
            services.AddTransient<IParteDAO, ParteDAO>();
            services.AddTransient<IRequerimientoDAO, RequerimientoDAO>();
            services.AddTransient<ISolicitudDAO, SolicitudDAO>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "talleres", Version = "v1" });
            });

            //Habilitando politicas del CORS para el consumo del Backend como API
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
            //agregamos las politicas del CORS al app
            app.UseCors(_MyCors);

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
