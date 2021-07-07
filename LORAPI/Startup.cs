using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LORAPI.Models;

namespace LORAPI
{
    public class Startup
    {
        //string ConnectionString = @"Data Source=192.168.4.110\LOR\MSSQL,1433;Initial Catalog=LORDB;MultipleActiveResultSets=true;User ID=LORUser;Password=Passw0rd"; // Husk at encrypt password
        string ConnectionString = @"Data Source=localhost;Initial Catalog=LORDB;MultipleActiveResultSets=true;User ID=LORUser;Password=Passw0rd"; 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            // Cors Policy, allows any origin/header/method. CORS(Cross-Origin Resource Sharing) er en http-header baseret mechanisme,
            // som tilader en server at indikere hvilket andet oprindelse. CORS er nødvendigt, da det gør at vi kan kalde på api'en externt.
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.AddDbContext<LORContext>(options => options.UseSqlServer(ConnectionString));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Sørg for at vi bruger CORS.
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
