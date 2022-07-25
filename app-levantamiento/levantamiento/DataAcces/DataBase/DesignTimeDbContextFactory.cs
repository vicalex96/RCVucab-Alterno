using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace levantamiento.DataAccess.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LevantamientoDBContext>
    {
        public LevantamientoDBContext CreateDbContext(string[]? args)
        {
            var builder = new DbContextOptionsBuilder<LevantamientoDBContext>();
            var connectionString = "Server=localhost;Database=Levantamiento;Port=5432;User Id=postgres;Password=123456";
            builder.UseNpgsql(connectionString);
            return new LevantamientoDBContext(builder.Options);
        }
    }
}