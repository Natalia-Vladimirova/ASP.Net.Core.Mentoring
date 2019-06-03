using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NorthwindApp.Core.Interfaces;
using NorthwindApp.Core.Services;
using NorthwindApp.DAL.Infrastructure;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.DAL.Repositories;
using NorthwindApp.Services;
using NorthwindApp.Services.Configuration;
using NorthwindApp.Services.Interfaces;
using NorthwindApp.UI.Infrastructure.Builders;
using NorthwindApp.UI.Infrastructure.Configuration;
using NorthwindApp.UI.Infrastructure.Filters;
using NorthwindApp.UI.Infrastructure.Middleware;
using NorthwindApp.UI.Interfaces;
using NorthwindApp.UI.Services;

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
            services.Configure<Breadcrumb>(options => _configuration.GetSection("SiteMap").Bind(options));
            services.Configure<ProductPageOptions>(_configuration);
            services.Configure<CategoryImageOptions>(_configuration);
            services.Configure<LoggingOptions>(_configuration);
            services.Configure<FileCacheOptions>(_configuration);
            services.Configure<AuthMessageSenderOptions>(_configuration);
            services.Configure<AzureAdGroupConfig>(options => _configuration.Bind("AzureAdGroupConfig", options));

            services.AddDbContext<NorthwindDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("NorthwindConnection")));
            services.AddDefaultIdentity<IdentityUser>(config =>
            {
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<NorthwindDbContext>();

            services.AddScoped<DbContext>(x => x.GetService<NorthwindDbContext>());

            services.AddScoped<RoleService>();
            services.AddScoped<IActiveDirectoryProvider, ActiveDirectoryProvider>();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();

            services.AddScoped<ISiteMapBuilder, SiteMapBuilder>();

            services.AddSingleton<ILogger, AppInsightsLogger>();

            services.AddSingleton<IMimeHelper, MimeHelper>();
            services.AddSingleton<ICacheService, DirectoryCacheService>(
                x => new DirectoryCacheService(
                    x.GetService<IOptions<FileCacheOptions>>(),
                    x.GetService<IHostingEnvironment>().ContentRootPath));

            services.AddAutoMapper();

            services.AddAuthentication()
                .AddOpenIdConnect("AzureAD", "Azure AD", options =>
                {
                    var azureOptions = new AzureAdOptions();
                    _configuration.Bind("AzureAd", azureOptions);

                    options.ClientId = azureOptions.ClientId;
                    options.ClientSecret = azureOptions.ClientSecret;
                    options.Authority = $"{azureOptions.Instance}{azureOptions.TenantId}";
                    options.CallbackPath = azureOptions.CallbackPath;
                    options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                    options.RequireHttpsMetadata = false;
                    options.UseTokenLifetime = true;
                    options.SaveTokens = true;
                });

            services.AddMvc(options => options.Filters.Add<LoggingFilter>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger logger)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

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

            app.UseAuthentication();

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
