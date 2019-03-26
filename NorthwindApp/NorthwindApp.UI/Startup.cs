using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthwindApp.Core.Interfaces;
using NorthwindApp.Core.Services;
using NorthwindApp.DAL.Infrastructure;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.DAL.Repositories;
using NorthwindApp.Services;
using NorthwindApp.Services.Interfaces;
using NorthwindApp.UI.Infrastructure.Filters;
using NorthwindApp.UI.Infrastructure.Middleware;
using NorthwindApp.UI.Interfaces;
using NorthwindApp.UI.Services;
using IConfigurationProvider = NorthwindApp.Core.Interfaces.IConfigurationProvider;
using ConfigurationProvider = NorthwindApp.Core.Providers.ConfigurationProvider;

namespace NorthwindApp.UI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NorthwindDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("NorthwindConnection")));
            services.AddScoped<DbContext>(x => x.GetService<NorthwindDbContext>());

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();

            services.AddSingleton<IConfigurationProvider, ConfigurationProvider>();
            services.AddSingleton<ILogger, AppInsightsLogger>();

            services.AddSingleton<IMimeHelper, MimeHelper>();
            services.AddSingleton<ICacheService, DirectoryCacheService>(
                x => new DirectoryCacheService(
                    x.GetService<IConfigurationProvider>(),
                    x.GetService<IHostingEnvironment>().ContentRootPath));

            services.AddAutoMapper();

            services.AddMvc(options => options.Filters.Add<LoggingFilter>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger logger)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/500");
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US", "en-US")
            });

            app.UseStaticFiles();
            app.UseImagesCache();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "images",
                    template: "images/{id:int}",
                    defaults: new { controller = "Category", action = "Image" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var properties = new Dictionary<string, string>
            {
                { "ApplicationLocation", env.ContentRootPath }
            };
            logger.LogInfo("Application start", properties);
        }
    }
}
