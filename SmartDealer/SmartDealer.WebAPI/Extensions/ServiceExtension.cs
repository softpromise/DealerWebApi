using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartDealer.Repository.Contexts;
using SmartDealer.Repository.Repositories;
using SmartDealer.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDealer.WebAPI.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureEntityFramework(this IServiceCollection services, IConfiguration Configuration)
        {

            services
               .AddEntityFrameworkSqlServer()
               .AddOptions()
               .AddDbContext<DealerContext>(options =>
                                            options.UseSqlServer(Configuration.GetConnectionString("SmartDealerContext"),
                                            optionBuilder => optionBuilder.MigrationsAssembly("SmartDealer.Repository")
                                            ));
        }
        public static void ConfigureCors(this IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public static void ConfigureRepositoryServices(this IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

        }

        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAuthService, AuthService>();

        }
    }
}
