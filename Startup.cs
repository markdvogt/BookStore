using BookStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; set; }//can name this whatever we want but standard is to name it Configuration

        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); //tells ASP.NET to use the MVC pattern

            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:MyConnection"]);
            });

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();

            services.AddRazorPages();

            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();  //tells ASP.NET to use the files in the wwwroot folder
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //This endpoint will be for if the user types in both a book category and a web page number
                endpoints.MapControllerRoute(
                    "categoryPage",
                    "{bookCategory}/Page{pageNum}",
                    new { Controller = "Home", action = "Index"});

                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}", //Now URL will show "Page1"
                    defaults: new {Controller = "Home", action = "Index", pageNum = 1}); //default pageNum is 1

                // This endpoint will be for if a user only selects a book category
                endpoints.MapControllerRoute(
                    "category",
                    "{bookCategory}",
                    new { Controller = "Home", action = "Index", pageNum = 1 }); //We have to input a default page number since the user didn’t specify

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });
        }
    }
}
