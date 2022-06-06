# Swagger-Dokumentasi
contoh swagger dokumentasi termasuk request sample
 langkahnya 
 
 1. buat project web api dgn cmd 
dotnet new webapi -lang "c#" -au none -o Template --framework "netcoreapp2.2"

 2. install depedency Swashbuckle.AspNetCore.Filters , Swashbuckle.AspNetCore.Annotations, Swashbuckle.AspNetCore
 hasil di .csproj sbb:
 <Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    <AspNetCoreHostingModel>InProcess</AspNetCoreHostingModel>
    <GenerateDocumentationFile>True</GenerateDocumentationFile>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.AspNetCore.Razor.Design" Version="2.2.0" PrivateAssets="All" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="4.0.1" />
    <PackageReference Include="Swashbuckle.AspNetCore.Annotations" Version="4.0.1" />
    <PackageReference Include="Swashbuckle.AspNetCore.Filters" Version="4.0.1" />
  </ItemGroup>

</Project>


3. setting startup
sbb:
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
        
4. setting controller. contoh sbb
  /// <summary>
        /// testing documentation SUTANTO
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [HttpGet("GetSUtanto")]
        public ActionResult<IEnumerable<string>> GetSUtanto(SutantoVM req)
        {
            return new string[] { "value1", "value2" };
        }

  
5. buat view model sutantoVM. sbb 
  public class SutantoVM
    {
        /// <example>Sutanto</example>
        public string firstName { get; set; }
        /// <example>Gasali</example>
        public string lastName { get; set; }


    }
 
  6. setting launchSetting.json
  {
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "iisSettings": {
    "windowsAuthentication": false, 
    "anonymousAuthentication": true, 
    "iisExpress": {
      "applicationUrl": "http://localhost:59782",
      "sslPort": 44388
    }
  },
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "swagger",
    //  "launchUrl": "api/values",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "Template": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "swagger",
     // "launchUrl": "api/values",
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
  
7. silahkan control + F5. liat swagger
