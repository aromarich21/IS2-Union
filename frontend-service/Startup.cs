using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using frontend_service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;

namespace frontend_service
{
    public class Startup
    {
        static public string appName = "IS2-UNION";
        static public string version = "0.7.8";
        static public string versionShow = "v " + version + " alpha";
        static public string _uptime = DateTime.Now.ToString();
        static public List<ClientDataFront> clientData;
        static public QAdata qaData = new QAdata();
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            clientData = new List<ClientDataFront>();
        }
       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            //app.UseStatusCodePagesWithRedirects("/Error");
            app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
