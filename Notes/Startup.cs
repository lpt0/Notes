/** Startup.cs
 * 
 * A lot of the code here is automatically generated by Visual Studio.
 * This file takes care of starting the server process and database 
 * connection(s). It also acts as a configuration file, to configure how
 * routing is handled by ASP.NET Core (such as automatically redirecting to
 * HTTPS, or serving static files).
 * The database connection string is also set up here. This is needed so that
 * Entity Framework Core can generate the controllers and views specified in
 * the context class - that library uses the connection string specified here
 * to connect to the database and set up the tables and relationships.
 * 
 * Author: Haran
 * Date: December 2nd, 2020
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;

// needed for CompatiblityVersion
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Import models for use
using Notes.Models;

// Following imports are required to be able to use EF Core
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Notes
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
            services.AddControllersWithViews();

            // enable compatability with ASP.NET Core 2.1 (required)
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // disable endpoint routing; this was an issue upgrading from ASP.Net Core 2.0 to 3.0
            services.AddMvc(options => options.EnableEndpointRouting = false);

            /* create the connection string, to connect to the database
             * `@` prefix means it is a string literal - it ignores escape
             * sequences (anything with a backslash) 
             * Connect to a local database named "Notes", using a trusted 
             * connection, and not retrying if it fails (since it is a local
             * database, and not a remote one, it should not fail).
             */
            string connection = @"Server=(localdb)\mssqllocaldb;Database=Notes;Trusted_Connection=True;ConnectRetryCount=0";

            /* Having one context instead of two+ makes it easier to link 
             * tables together */
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                /* if compiled and run in debug mode, uncaught exceptions 
                 * show an error page with the stack trace of the exception
                 */
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection(); // force usage of https
            app.UseStaticFiles(); // serve files out of wwwroot ('~/' within pages)

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
