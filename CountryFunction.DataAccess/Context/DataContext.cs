using CountryFunction.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace CountryFunction.DataAccess.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Country> country { get; set; }
    }
}
