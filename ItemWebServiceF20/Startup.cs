using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace ItemWebServiceF20
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Items API", 
                                                                                   Version = "V1.0", 
                                                                                   Description = "Api for cosumming items", 
                                                                                   Contact = new OpenApiContact(){Email = "{ jaef@easj.dk}", Name = "{Jamshid}"}
                                                                                   });
                                            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                                            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                                            c.IncludeXmlComments(xmlPath);
                                          }
                                      );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Items API version 1.0"));
            //app.UseSwaggerUI(c => c.RoutePrefix = "api/Help");
            app.UseMvc();
        }
    }
}
