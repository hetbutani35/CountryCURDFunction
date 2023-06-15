using CountryFunction.DataAccess.Context;
using CountryFunction.DataAccess.Implementation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CountryFunction.Startup))]

namespace CountryFunction
{
    //public class Startup : DbContext
    //{
    //    private readonly IConfiguration _configuration;
    //    public Startup(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
    //    {
    //        _configuration = configuration;
    //    }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        if (!optionsBuilder.IsConfigured)
    //        {
    //            optionsBuilder.UseNpgsql(_configuration["host=localhost port=5432 dbname=postgres user=postgres password=123 sslmode=prefer connect_timeout=10"]);
    //        }
    //    }
    //}

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = "host=localhost; Database=postgres; username=postgres; password=123; port=5432";
            builder.Services.AddDbContext<DataContext>(x =>
            {
                x.UseNpgsql(connectionString
                , options => options.EnableRetryOnFailure());
            });

            builder.Services.AddTransient<ICRUDService, CRUDService>();
        }
    }
}
