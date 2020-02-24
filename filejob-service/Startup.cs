using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filejob_service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace filejob_service
{
    public class Startup
    {
        public string version = "0.4.5";
        static public List<SourceElements> sourceCurElements; //sources
        static public List<SourceLinks> sourceCurLinks;
        static public List<SourceElements> sourceIntElements;
        static public List<SourceLinks> sourceIntLinks;
        static public List<SourceElements> sourceResElements;
        static public List<SourceLinks> sourceResLinks;

        static public List<SourceClientData> sourceClientData;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            CreateSources();
            TestFunction();
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            /*
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
                // you can add more options here and they will be applied to all cookies (middleware and manually created cookies)
            });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
            SourceUnits sourceUnits= new SourceUnits(element,link);
            SourceClientData source1ClientData = new SourceClientData("test", sourceUnits, sourceUnits, sourceUnits);
            sourceClientData = new List<SourceClientData>();
            sourceClientData.Add(source1ClientData);
        }
    }
}
