using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiObjects.Models;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace WebApiObjects
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddOData(opt => opt.Count()
                                                    .Filter()
                                                    .OrderBy()
                                                    .Expand()
                                                    .Select()
                                                    .AddRouteComponents("v", GetEdmModel()));

            services.AddDbContext<WebDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("WebDbContext"))
                //.UseLazyLoadingProxies()
            );

           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "test",
                    pattern: "test",
                    defaults: new { controller = "Models", action = "sayhello" }
                );

                endpoints.MapControllerRoute(
                    name: "test",
                    pattern: "test/data",
                    defaults: new { controller = "Models", action = "addsampledata" }
                );

                endpoints.MapControllerRoute(
                    name: "test2",
                    pattern: "test/retrieve",
                    defaults: new { controller = "Models", action = "retrievemodels" }
                );

            });
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Model>("Models").EntityType.HasMany(m => m.SubModel);
            builder.EntitySet<Property>("Propterties");//.EntityType.HasKey(prop => prop.ParentModel);
            return builder.GetEdmModel();
        }
    }
}
