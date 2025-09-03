using FINANCE.TRACKER.Data;
using FINANCE.TRACKER.Models;
using FINANCE.TRACKER.Services.Category.Implementations;
using FINANCE.TRACKER.Services.Category.Interfaces;
using FINANCE.TRACKER.Services.UserManager.Implementations;
using FINANCE.TRACKER.Services.UserManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.TRACKER
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "BudgetManager_";
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true; // Set the cookie to be HttpOnly
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });

            builder.Services.AddHttpContextAccessor();

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<ViewModuleFilter>();
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection"));
            });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IModuleService, ModuleService>();
            builder.Services.AddScoped<IActionService, ActionService>();
            builder.Services.AddScoped<IModuleActionService, ModuleActionService>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();
            builder.Services.AddScoped<IModuleAccessService, ModuleAccessService>();
            builder.Services.AddScoped<IBudgetCategoryService, BudgetCategoryService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Dashboard/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
