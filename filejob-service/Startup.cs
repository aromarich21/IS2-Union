using System.Collections.Generic;
using filejob_service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace filejob_service
{
    public class Startup
    {
        public string version = "0.4.7";
        static public List<SourceElements> sourceCurElements; //sources
        static public List<SourceLinks> sourceCurLinks;
        static public List<SourceElements> sourceIntElements;
        static public List<SourceLinks> sourceIntLinks;
        static public List<SourceElements> sourceResElements;
        static public List<SourceLinks> sourceResLinks;

        static public List<ClientData> sourceClientData;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            sourceClientData = new List<ClientData>();
            CreateSources();
            TestFunction();
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
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
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller}/{action=Index}/{function?}/{attribute?}/{id?}");
            });
        }

        public void CreateSources()
        {
            sourceCurElements = new List<SourceElements>();
            sourceCurLinks = new List<SourceLinks>();
            sourceIntElements = new List<SourceElements>();
            sourceIntLinks = new List<SourceLinks>();
            sourceResElements = new List<SourceElements>();
            sourceResLinks = new List<SourceLinks>();
        }

        public void TestFunction()
        {
            Elements element = new Elements("test","1","level", "number", "status", " type", "formalization");
            Links link = new Links("afe1", "afe2", "afe3", "test");
            Units sourceUnits= new Units(element,link);
            ClientData source1ClientData = new ClientData("test", sourceUnits, sourceUnits, sourceUnits);
            //sourceClientData.Add(source1ClientData);
            ClientDataJob clientDataJobAdd = new ClientDataJob("token_test",sourceClientData);
            clientDataJobAdd.AddElement(sourceClientData,"cur",element);
        }
    }
}