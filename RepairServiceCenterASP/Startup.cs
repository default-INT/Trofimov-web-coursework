using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepairServiceCenterASP.Data;
using RepairServiceCenterASP.Middleware;
using RepairServiceCenterASP.Models;
using RepairServiceCenterASP.Services;

namespace RepairServiceCenterASP
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RepairServiceCenterContext>(options => options.UseSqlServer(connection));
            services.AddMemoryCache();
            services.AddSession();
            services.AddTransient<ICachingModel<RepairedModel>, RepeiredModelService>();
            
            services.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AccountContextConnection")));

            // добавление сервисов Idenity
            services.AddIdentity<AccountUser, IdentityRole>()
                .AddEntityFrameworkStores<AccountContext>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            app.UseDbInitializer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
