// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
// Comment out the next line to use CosmosDb instead of InMemory for the anchor cache.
#define INMEMORY_DEMO

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharingService.Core.Services.Anchors;
using SharingService.Core.Services.Token;
using SharingService.Data.EntityFramework;
using SharingService.Data.Service;
using SharingService.Web.Core.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;

namespace SharingService.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var coreAssembly = Assembly.Load("SharingService.Web.Core");

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddApplicationPart(coreAssembly);

            services.AddRouting(options => options.LowercaseUrls = true);

            var persistenceConfig = Configuration
                .GetSection("Persistence")
                .Get<PersistenceConfig>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = $"{nameof(SharingService)} API", Version = "v1" });
            });

            switch (persistenceConfig.Provider)
            {
                case PersistenceProvider.Sqlite:
                    services.AddDbContext<SharingServiceContext>(options => options.UseSqlite(
                        persistenceConfig.ConnectionString,
                        builder => builder.MigrationsAssembly("SharingService.Data.EntityFramework.Sqlite")
                        ));
                    services.AddTransient<IAnchorRepository, Data.EntityFramework.Service.AnchorRepository>();
                    break;
                case PersistenceProvider.SqlServer:
                    services.AddDbContext<SharingServiceContext>(options => options.UseSqlServer(
                        persistenceConfig.ConnectionString,
                        builder => builder.MigrationsAssembly("SharingService.Data.EntityFramework.SqlServer")
                        ));
                    services.AddTransient<IAnchorRepository, Data.EntityFramework.Service.AnchorRepository>();
                    break;
                case PersistenceProvider.Cosmos:
                    services.AddDbContext<SharingServiceContext>(options => options.UseCosmos(
                        persistenceConfig.ConnectionString,
                        persistenceConfig.AccessKey,
                        persistenceConfig.DatabaseName));
                    services.AddTransient<IAnchorRepository, Data.EntityFramework.Service.AnchorRepository>();
                    break;
                case PersistenceProvider.InMemory:
                    services.AddDbContext<SharingServiceContext>(options => options.UseInMemoryDatabase());
                    services.AddTransient<IAnchorRepository, Data.EntityFramework.Service.AnchorRepository>();
                    break;
                default:
                    throw new InvalidOperationException("Cannot start the app without configuring a proper persistence context");
            }

            services.AddHttpContextAccessor();

            services.AddTransient<IAnchorService, AnchorService>();
            services.AddHttpClient<ITokenService, TokenService>();
            services.AddSingleton<TokenServiceSettings>(_ =>
            {
                return new TokenServiceSettings
                {
                    SpatialAnchorsAccountId = "<Spatial Anchors Account Id>",
                    SpatialAnchorsResource = "https://sts.mixedreality.azure.com",
                    AadClientId = "<AAD client id>",
                    AadClientKey = "<AAD client key>",
                    AadTenantId = "<AAD Tenant ID>"
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{nameof(SharingService)} API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();

            var persistenceConfig = Configuration
                .GetSection("Persistence")
                .Get<PersistenceConfig>();

            if (persistenceConfig.Provider == PersistenceProvider.Cosmos)
            {
                var context = app.ApplicationServices.GetService<SharingServiceContext>();
                context.Database.EnsureCreated();
            }                
        }
    }
}
