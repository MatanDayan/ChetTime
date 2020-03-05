using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ChetTime.Db;
using ChetTime.Hubs;
using ChetTime.Models;
using ChetTime.Services;
using System.Net;

namespace ChetTime
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
            services.AddSingleton<IChatHistoryService, ChatHistoryService>();

            services.AddCors();

            services.AddRazorPages();
            services.AddSignalR();

            services.AddControllersWithViews();
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 3;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
               
            }).AddEntityFrameworkStores<Db.DbContext>();


            services.AddDbContext<Db.DbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Myconnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , Db.DbContext db)
        {

            db.Database.EnsureCreated();


            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();
            app.UseSignalR(route=>route.MapHub<ChatHub>("/chatHub"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                
                    endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=User}/{action=Login}/{id?}");

               
               //endpoints.MapRazorPages();
               //endpoints.MapHub<ChatHub>("/chatHub");

                

            });

            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                response.StatusCode == (int)HttpStatusCode.NotFound)
                    response.Redirect("/Error/Unauthorized");
            });
        }
    }
}
