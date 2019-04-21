using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using ConfigurationProvider = NorthwindApp.Core.Providers.ConfigurationProvider;
using IConfigurationProvider = NorthwindApp.Core.Interfaces.IConfigurationProvider;

namespace NorthwindApp.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NorthwindDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("NorthwindConnection")));
            services.AddScoped<DbContext>(x => x.GetService<NorthwindDbContext>());

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddSingleton<IConfigurationProvider, ConfigurationProvider>();
            services.AddSingleton<IMimeHelper, MimeHelper>();

            services.AddAutoMapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMvc();
        }
    }
}
