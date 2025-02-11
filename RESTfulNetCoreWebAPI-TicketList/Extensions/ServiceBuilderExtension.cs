using RESTfulNetCoreWebAPI_TicketList.Data;
using RESTfulNetCoreWebAPI_TicketList.Helpers;
using RESTfulNetCoreWebAPI_TicketList.Repositories;
using RESTfulNetCoreWebAPI_TicketList.Services;
using System.Diagnostics.CodeAnalysis;

namespace RESTfulNetCoreWebAPI_TicketList.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceBuilderExtension
    {
        public static void AppServiceBuilder(this IServiceCollection servicesBuilder)
        {
            servicesBuilder.AddScoped<TicketDataSeeder>();
            servicesBuilder.AddTransient<ITicketRepository, TicketRepository>();
            servicesBuilder.AddTransient<ITicketService, TicketService>();
            servicesBuilder.AddTransient<IFibonacciSequence, FibonacciSequence>();
            servicesBuilder.AddTransient<IPalindromeWords, PalindromeWords>();
            servicesBuilder.AddTransient<IMiscellaneousService, MiscellaneousService>();
        }
    }
}
