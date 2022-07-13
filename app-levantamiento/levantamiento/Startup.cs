using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using levantamiento.Persistence.DAOs;
using levantamiento.Persistence.Database;
using levantamiento.BussinesLogic.Logic;
using levantamiento.Conections.APIs;
using levantamiento.Conections.rabbit;

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
            services.AddTransient<IConsumerRabbit, ConsumerRabbit>();
            services.AddTransient<IProductorRabbit, ProductorRabbit>();


            services.AddTransient<IIncidenteDAO, IncidenteDAO>();
            services.AddTransient<ISolicitudReparacionDAO, SolcitudReparacionDAO>();
            services.AddTransient<IRequerimientoDAO, RequerimientoDAO>();
            services.AddTransient<IParteDAO, ParteDAO>();
            


            services.AddTransient<IIncidenteLogic, IncidenteLogic>();
            services.AddTransient<ISolicitudReparacionLogic, SolicitudReparacionLogic>();
            services.AddTransient<IRequerimientoLogic, RequerimientoLogic>();
            services.AddTransient<IParteLogic, ParteLogic>();

            services.AddTransient<IVehiculoAPI, VehiculoAPI>();
            services.AddTransient<IIncidenteAPI, IncidenteAPI>();


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