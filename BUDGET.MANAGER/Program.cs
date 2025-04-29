using BUDGET.MANAGER.Data;
using BUDGET.MANAGER.Services.Interfaces;
using BUDGET.MANAGER.Services.UserManager.Implementations;
using BUDGET.MANAGER.Services.UserManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BUDGET.MANAGER
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
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

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Dashboard}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
