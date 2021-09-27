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
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                    builder =>
                                    {
                                        builder.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader();
                                    });
            });

            //services.AddControllers().AddOData(opt => opt.Count()
            //                                        .Filter()
            //                                        .OrderBy()
            //                                        .Expand()
            //                                        .Select()
            //                                        .AddRouteComponents("v", GetEdmModel()));
            services.AddControllers();
            services.AddDbContext<WebDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("WebDbContext"))
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

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "test",
                    pattern: "test/data",
                    defaults: new { controller = "Models", action = "addsampledata" }
                );

                endpoints.MapControllerRoute(
                    name: "projects",
                    pattern: "projects/create",
                    defaults: new { controller = "Projects", action = "createproject" }
                );

                endpoints.MapControllerRoute(
                    name: "projects",
                    pattern: "projects",
                    defaults: new { controller = "Projects", action = "getprojects" }
                );

                endpoints.MapControllerRoute(
                    name: "models",
                    pattern: "models",
                    defaults: new { controller = "Models", action = "getmodels" }
                );

                endpoints.MapControllerRoute(
                    name: "models",
                    pattern: "models/create",
                    defaults: new { controller = "Models", action = "createmodel" }
                );

                endpoints.MapControllerRoute(
                    name: "models",
                    pattern: "models/retrieve/{model}",
                    defaults: new { controller = "Models", action = "retrievemodels" }
                );

                endpoints.MapControllerRoute(
                    name: "types",
                    pattern: "types/retrieve/{type}",
                    defaults: new { controller = "ModelTypes", action = "retrievetype" }
                );

            });
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Model>("Models").EntityType.HasMany(m => m.children);
            builder.EntitySet<Property>("Propterties");//.EntityType.HasKey(prop => prop.ParentModel);
            return builder.GetEdmModel();
        }
    }
}
