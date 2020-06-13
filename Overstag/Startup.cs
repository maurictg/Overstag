using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Overstag.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;
using Microsoft.AspNetCore.StaticFiles;

namespace Overstag
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")));
            services.AddMvc();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();
            services.AddResponseCaching();
            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddResponseCompression(options => {
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true; 
            });

            services.AddSocketHandler(); //custom, Sockets.cs
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error/500");

            app.UseStatusCodePagesWithRedirects("/Error/{0}");

            //Settings
            app.UseHttpsRedirection();
            app.UseResponseCaching();
            app.UseResponseCompression();
            app.UseSession();

            var fep = new FileExtensionContentTypeProvider();
            fep.Mappings[".less"] = "plain/text";

            app.UseStaticFiles(new StaticFileOptions() { ContentTypeProvider = fep });
            app.UseRouting();

            app.UseWebSockets();
            app.MapSocketHandler("/ws", provider.GetService<SocketHandler>());

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }

    }
}
