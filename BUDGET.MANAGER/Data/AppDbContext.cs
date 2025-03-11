using BUDGET.MANAGER.Models.UserManager;
using Microsoft.EntityFrameworkCore;

namespace BUDGET.MANAGER.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public DbSet<RoleModel> Roles{ get; set; }

        public DbSet<UserRoleModel> UserRoles { get; set; }

        public DbSet<ActionModel> Actions { get; set; }

        public DbSet<ModuleModel> Modules { get; set; }

        public DbSet<ModuleActionModel> ModuleActions { get; set; }

        public DbSet<ModuleAccessModel> ModuleAccess { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoleModel>().HasIndex(e => new { e.UserId }).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
