using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drones
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings.Remove("MysqlConnection");
            config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("MysqlConnection", "Data Source=localhost;port=3306;Initial Catalog=drone_control_v1; User Id=root;password=;SslMode=none;convert zero datetime=True"));     
            //config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("MysqlConnection", "Data Source=verynicedatabase.000webhostapp.com;port=3306;Initial Catalog=id13561070_drones; User Id=id13561070_root;password=Abc123456789*;SslMode=none;convert zero datetime=True"));
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            Configuration = configuration;

            Controllers.DroneSubsystemController.receiveLicensePlateInfo();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ParkingLot}/{action=Index}/{id?}");
            });
        }
    }
}
