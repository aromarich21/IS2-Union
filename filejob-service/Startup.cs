using System;
using System.Collections.Generic;
using System.Threading;
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
        public string version = "0.6.1";
        static public List<ClientData> sourceClientData;
        public static void ClearClientData()
        {
            var count = 0;
            foreach (ClientData item in sourceClientData)
            {
                sourceClientData.Remove(item);
            }
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            sourceClientData = new List<ClientData>();
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