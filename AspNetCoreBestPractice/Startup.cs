using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using Shared;
using Repositories.Dapper;
using AspNetCoreBestPractice.Hubs;
using Microsoft.AspNetCore.Sockets;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCoreBestPractice
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);


           

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSignalRCore();

            var connString = Configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<IDbConnection>((_) => {
                var conn =  new MySqlConnection(connString); //Can swap with MsSqlConnection or other AnsiCompliant ADO.net Connection
                conn.Open();
                return conn;
            });

            services.AddTransient<ConnectionManager, ConnectionManager>();
            services.AddTransient<HttpConnectionDispatcher, HttpConnectionDispatcher>();


            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IEventRepository>(s =>{
                var conn = s.GetService<IDbConnection>();
                var hub = s.GetService<IHubContext<EventHub>>();
                var repo = new EventRepository(conn);
                repo.AddOnSaved((item) => {
                    hub.Clients.All.InvokeAsync("receiveEvent", item);
                });
                return repo;
            });


           

        }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
  
            app.UseSignalR(routes =>
            {
                routes.MapHub<EventHub>("eventhub");
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
