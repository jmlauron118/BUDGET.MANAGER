using BUDGET.MANAGER.Models.UserManager;
using Microsoft.EntityFrameworkCore;

namespace BUDGET.MANAGER.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> UserModels { get; set; }

        public DbSet<RoleModel> RoleModels { get; set; }

        public DbSet<UserRoleModel> UserRoleModels { get; set; }

        public DbSet<ActionModel> ActionModels { get; set; }

        public DbSet<ModuleModel> ModuleModels { get; set; }

        public DbSet<ModuleActionModel> ModuleActionModels { get; set; }

        public DbSet<ModuleAccessModel> ModuleAccessModels { get; set; }

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
