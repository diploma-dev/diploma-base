using DiplomaProject.DatabaseSecret;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly IDatabaseSecret databaseSecret = new DatabaseSecret.DatabaseSecret();

        public static void ConfigureDataAccessServices(this IServiceCollection services,
            Action<DataAccessOptions> optionsSetter = default!)
        {
            services.AddSingleton(databaseSecret);
            services.AddDbContext<AppDbContext>(options =>
            {
                var dataAccessOptions = new DataAccessOptions();

                optionsSetter?.Invoke(dataAccessOptions);

                var connectionString = databaseSecret.GetConnectionString();
                var collectSqlQueries = dataAccessOptions.CollectSqlQueries;


                options.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly("Migrations"));
            });

            services.AddScoped<ServiceDbContext, AppDbContext>();
        }
    }

    public class DataAccessOptions
    {
        public bool CollectSqlQueries { get; set; }
    }
}
