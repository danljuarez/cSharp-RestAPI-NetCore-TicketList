using Microsoft.OpenApi.Models;
using RESTfulNetCoreWebAPI_TicketList.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace RESTfulNetCoreWebAPI_TicketList
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to register repositories
            services.AppServiceBuilder();

            // Add services to the container.
            services.AddControllers();

            // Register AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configuring Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RESTfulNetCoreWebAPI-TicketList",
                    Description = "Author: Daniel Juarez"
                });
            });

            // Disable Cross-Origin Requests (CORS) - For the purpose of this sampler. Do not use this in production environments.
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Disable Cross-Origin (CORS) - For the purpose of this sampler. Do not use this in production environments.
            app.UseCors(options => options
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true) // Allow any origin
                                                            //.WithOrigins("http://localhost:3000")); // Allow only this origin can also have multiple origins separated with commas
                        .AllowCredentials());

            app.UseAuthorization();
        }
    }
}
