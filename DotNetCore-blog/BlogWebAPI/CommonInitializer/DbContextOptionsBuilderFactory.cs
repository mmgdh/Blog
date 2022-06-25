using Microsoft.EntityFrameworkCore;

namespace CommonInitializer
{
    public static class DbContextOptionsBuilderFactory
    {
        public static DbContextOptionsBuilder<TDbContext> Create<TDbContext>()
            where TDbContext : DbContext
        {
            var connStr = Environment.GetEnvironmentVariable("DefaultDB:ConnStr");
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=Blog;Trusted_Connection=True");
            //optionsBuilder.UseSqlServer(connStr);
            return optionsBuilder;
        }
    }
}
