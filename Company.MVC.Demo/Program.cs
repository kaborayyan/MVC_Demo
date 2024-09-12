using Company.MVC.Demo.BLL.Interface;
using Company.MVC.Demo.BLL.Repository;
using Company.MVC.Demo.DAL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Company.MVC.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Allow Dependency Injection for AppDbContext
            // builder.Services.AddScoped<AppDbContext>();
            // there are also AddSingleton and AddTransient

            // ASP has a more direct method
            // Same effect as the above code
            // Allow Dependency Injection for AppDbContext
            // This means CLR can create an object from AppDbContext at any time
            // options here of type dbContextOptionBuilder
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            
            // To allow dependency injection for the repository
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
