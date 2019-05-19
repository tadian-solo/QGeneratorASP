using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QGeneratorASP.Models;

namespace QGeneratorASP
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
            // services.AddDbContext<GQ>(opt => opt.UseInMemoryDatabase("GQ"));
            var connection = Configuration.GetConnectionString("DefaultConnection");

            /* services.AddIdentity<User, IdentityRole>()
                 .AddEntityFrameworkStores<GQ>();*/

            services.AddIdentity<User, IdentityRole<int>>()
           .AddEntityFrameworkStores<GQ>()
           .AddDefaultTokenProviders();

            services.AddDbContext<GQ>(options => options.UseSqlServer(connection));

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "SimpleWebApp";
                options.LoginPath = "/";
                options.AccessDeniedPath = "/";
                options.LogoutPath = "/";
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
        }
        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager =
            serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager =
            serviceProvider.GetRequiredService<UserManager<User>>();
            // Создание ролей администратора и пользователя
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("user"));
            }
            // Создание Администратора
            string adminLogin = "admin";
            string adminPassword = "Aa123456!";
            if (await userManager.FindByNameAsync(adminLogin) == null)
            {
                User admin = new User
                {

                    UserName = adminLogin,
                    AccessLevel = true
                };
                IdentityResult result = await
                userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            // Создание Пользователя
            string userEmail = "user";
            string userPassword = "Aa123456!";
            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                User user = new User
                {
                   
                    UserName = userEmail,
                    AccessLevel = false
                };
                IdentityResult result = await
                userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                {

                   await userManager.AddToRoleAsync(user, "user");
                }
            }
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            CreateUserRoles(services).Wait();
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); } else { app.UseHsts(); }
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
