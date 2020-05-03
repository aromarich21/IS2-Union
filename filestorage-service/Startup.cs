using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace filestorage_service
{
    public class Startup
    {
        static public string version = "0.7.0";
        static public string nameService = "filestorage-service";
        public static string directoryFiles = @"Files";
        public static List<Models.FileInfo> fileStorage = new List<Models.FileInfo>();
        public static List<string> Test = new List<string>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ClearDirectory(directoryFiles);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
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
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ClearDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir);
                foreach (string item in files)
                {
                    File.Delete(item);
                    /*
                    if (fileStorage.Find((x) => x.Name == item) == null)
                    {
                        Models.FileInfo file = new Models.FileInfo(item, null);
                        fileStorage.Add(file);
                    }*/
                }
            }
        }

        public static void CheckDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir);
                foreach (string item in files)
                {
                    if (fileStorage.Find((x) => x.Name == item) == null)
                    {
                        Models.FileInfo file = new Models.FileInfo(item, null);
                        fileStorage.Add(file);
                    }
                }
            }
        }
    }
}
