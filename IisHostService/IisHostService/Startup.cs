using System;
using System.IO;
using IisHostService.DAL.Contexts;
using IisHostService.Serivces;
using IisHostService.Serivces.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace IisHostService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public IConfigurationRoot ConfigurationRoot { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(webHostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{webHostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
            ConfigurationRoot = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //  configure DB contexts
            services.AddDbContext<OrderContext>(options =>
                options.UseInMemoryDatabase("TestDataBase"));

            //  configure services
            services.AddScoped<IHelloWorldService, HelloWorldService>();

            //  configure infrastructure and other
            services.AddControllers();
            services.AddSwaggerGen(opt =>
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "IisHostService", Description = "Just api" }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHelloWorldService helloWorldService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "IisHostService_v1");
                opt.RoutePrefix = String.Empty;
            });

            ConfigureMiddleware(app);
        }

        private void ConfigureMiddleware(IApplicationBuilder app)
        {
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello, World!");
            //    using (var stream = new StreamReader(context.Request.Body))
            //    {
            //        var response = await stream.ReadToEndAsync();
            //    }

            //    foreach (var query in context.Request.Query)
            //    {
            //        string valeus = $"{query.Key}  {query.Value}";
            //    }

            //    await next.Invoke();
            //});
        }
    }
}
