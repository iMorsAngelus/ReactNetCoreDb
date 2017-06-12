﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ReactNetCoreDB.Models;

namespace ReactNetCoreDB
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            //Scaffold-DbContext "Server=WS194;Database=AdventureWorks2014;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
            var connection = @"Server=.;Database=AdventureWorks2014;Trusted_Connection=True;";
            services.AddDbContext<AdventureWorks2014Context>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();

            ////React start
            //app.UseReact(config =>
            //{
            //    // If you want to use server-side rendering of React components,
            //    // add all the necessary JavaScript files here. This includes
            //    // your components as well as all of their dependencies.
            //    // See http://reactjs.net/ for more information. Example:
            //    //config
            //    //  .AddScript("~/Scripts/First.jsx")
            //    //  .AddScript("~/Scripts/Second.jsx");

            //    // If you use an external build too (for example, Babel, Webpack,
            //    // Browserify or Gulp), you can improve performance by disabling
            //    // ReactJS.NET's version of Babel and loading the pre-transpiled
            //    // scripts. Example:
            //    config
            //        .AddScript("~/js/Table.jsx");
            //    //  .SetLoadBabel(false)
            //    //  .AddScriptWithoutTransform("~/Scripts/bundle.server.js");
            //});
        }
    }
}
