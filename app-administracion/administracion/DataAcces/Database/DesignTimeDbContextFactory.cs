using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using administracion.DataAccess.Database;


namespace administracion.DataAccess.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AdminDBContext>
    {
        public AdminDBContext CreateDbContext(string[]? args)
        {
            var builder = new DbContextOptionsBuilder<AdminDBContext>();
            var connectionString = "Server=localhost;Database=Administracion;Port=5432;User Id=postgres;Password=123456";
            builder.UseNpgsql(connectionString);
            return new AdminDBContext(builder.Options);
        }
    }
}