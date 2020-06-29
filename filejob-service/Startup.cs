using System;
using System.Collections.Generic;
using System.Xml.XPath;
using filejob_service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.Swagger;

namespace filejob_service
{
    public class Startup
    {
        static public string _uptime = DateTime.Now.ToString();
        static public string nameService = "filejob-service";
        static public string version = "0.7.1";
        static public List<SubjectElements> subjTest = new List<SubjectElements>();
        static public List<ClientData> sourceClientData;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            sourceClientData = new List<ClientData>();
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(GetXmlCommentsPath());
            });
        }

        private static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}\Swagger.xml", AppDomain.CurrentDomain.BaseDirectory);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IS2-Union API");
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller}/{action=Index}/{function?}/{attribute?}/{id?}");
            });
        }
    }
}