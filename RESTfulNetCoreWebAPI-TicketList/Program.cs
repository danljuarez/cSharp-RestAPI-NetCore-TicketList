using Microsoft.EntityFrameworkCore;
using RESTfulNetCoreWebAPI_TicketList.Data;
using System.Diagnostics.CodeAnalysis;

namespace RESTfulNetCoreWebAPI_TicketList
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TicketContext>(
                opt => opt.UseInMemoryDatabase("TicketInMemoryDB")
            );

            var startup = new Startup(builder.Configuration);

            startup.ConfigureServices(builder.Services);

            builder.Services.AddControllers()
                .AddNewtonsoftJson();

            var app = builder.Build();

            startup.Configure(app, app.Environment);

            InitializeDataSeed();

            app.MapControllers();

            app.Run();

            // Initialize data set used for this sample .Net Core web API application
            void InitializeDataSeed()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dataSeeder = scope.ServiceProvider.GetRequiredService<TicketDataSeeder>();
                    dataSeeder.SeedData().Wait();
                }
            }
        }
    }
}
