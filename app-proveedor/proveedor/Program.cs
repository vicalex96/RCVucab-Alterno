using proveedor.Persistence.Database;
using proveedor.Persistence.Entities;
using proveedor.Persistence.DAOs;
using proveedor.Persistence.DAOs.Interfaces;
using proveedor.Persistence.DAOs.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace proveedor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
