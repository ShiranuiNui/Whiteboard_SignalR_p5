﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Whiteboard_SignalR_p5.Contexts;
using Whiteboard_SignalR_p5.Hubs;
using Whiteboard_SignalR_p5.Models;

namespace Whiteboard_SignalR_p5
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CoordinatesContext>();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            using(var context = new CoordinatesContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            app.UseFileServer();
            app.UseSignalR(routes =>
            {
                routes.MapHub<WhiteboardHub>("draw");
            });
        }
    }
}