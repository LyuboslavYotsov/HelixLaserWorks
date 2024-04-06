using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IPartService, PartService>();
            services.AddScoped<IFileManageService, FileManageService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IThicknessService, ThicknessService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IReviewService, ReviewService>();

            return services;
        }


        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }


        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
