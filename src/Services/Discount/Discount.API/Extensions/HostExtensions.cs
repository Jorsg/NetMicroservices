using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Runtime.CompilerServices;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migration postresql database");
                    using var connetion = new NpgsqlConnection
                        (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connetion.Open();

                    using var commad = new NpgsqlCommand
                    {
                        Connection = connetion,
                    };

                    commad.CommandText = "DROP TABLE IF EXISTS Coupon";
                    commad.ExecuteNonQuery();

                    commad.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                                                               ProductName Varchar(24) NOT NULL,
                                                               Description TEXT,
                                                               Amount INT)";
                    commad.ExecuteNonQuery();

                    commad.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X','IPhone Discount','156')";
                    commad.ExecuteNonQuery();

                    commad.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10','Samsung Discount','113')";
                    commad.ExecuteNonQuery();

                    logger.LogInformation("Migrate postresql database");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogInformation(ex, "An error occurred while migrate the postresql database");
                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                    throw;
                }
            }
            return host;
        }

    }
}
