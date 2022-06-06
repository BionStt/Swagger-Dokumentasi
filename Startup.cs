using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Template
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

            //add cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    // .WithExposedHeaders("Content-Disposition")//sutanto
                    .AllowAnyHeader());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "OASE_BE", Version = "v1", Description = "Web Api" });

                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "OASE_BE", Version = "v1" });
                //var security = new Dictionary<string, IEnumerable<string>>
                //{
                //    {"Bearer", new string[] { }},
                //};
                //c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "apiKey"
                //});
                //c.AddSecurityRequirement(security);

                // Set the comments path for the XmlComments file.
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OASE_BE v1"));
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                // c.SwaggerEndpoint("/OaseEformIntegration/swagger/v1/swagger.json", "BackEndForLearning v1");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OASE_BE v1");
                //  c.SwaggerEndpoint("/OaseEformIntegration/swagger/v1/swagger.json", "BackEndForLearning v1");
                c.SwaggerEndpoint("v1/swagger.json", "OASE_BE v1");
                c.DocumentTitle = "Documentation";
                c.DocExpansion(DocExpansion.None);
            });


            app.UseMvc();
        }
    }
}
