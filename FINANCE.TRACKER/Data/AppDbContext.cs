using FINANCE.TRACKER.Models.Category;
using FINANCE.TRACKER.Models.UserManager;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.TRACKER.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public DbSet<RoleModel> Roles { get; set; }

        public DbSet<UserRoleModel> UserRoles { get; set; }

        public DbSet<ActionModel> Actions { get; set; }

        public DbSet<ModuleModel> Modules { get; set; }

        public DbSet<ModuleActionModel> ModuleActions { get; set; }

        public DbSet<ModuleAccessModel> ModuleAccess { get; set; }

        public DbSet<BudgetCategoryModel> BudgetCategories { get; set; }
        public DbSet<ExpensesCategoryModel> ExpenseCategories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserRoleModel>().HasAlternateKey(e => new { e.UserId, e.RoleId });
        //    modelBuilder.Entity<ModuleActionModel>().HasAlternateKey(e => new { e.ModuleId, e.ActionId });
        //    modelBuilder.Entity<ModuleAccessModel>().HasAlternateKey(e => new { e.ModuleActionId, e.UserRoleId });
        //    modelBuilder.Entity<ActionModel>().HasAlternateKey(e => new { e.ActionName });

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
