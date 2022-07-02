using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using levantamiento.Persistence.DAOs;
using levantamiento.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace levantamiento
{
    public class Startup
    {
        private readonly string  _MyCors ="Mycors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<LevantamientoDBContext>( 
                o => o.UseNpgsql(Configuration.GetConnectionString("cnDatabase"))
                );
                
            services.AddTransient<ILevantamientoDBContext, LevantamientoDBContext>();
            services.AddTransient<IIncidenteDAO, IncidenteDAO>();
            services.AddTransient<ISolicitudReparacionDAO, SolcitudReparacionDAO>
            ();
            services.AddTransient<IRequerimientoDAO, RequerimientoDAO>
            ();
            services.AddTransient<IParteDAO, ParteDAO>
            ();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "levantamiento", 
                    Version = "v1" 
                });
                
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