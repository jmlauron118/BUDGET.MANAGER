using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.Models.UserManager
{
    public class UserRoleModel
    {
        [Key]
        public int UserRoleId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public int CreateBy { get; set; }

        public DateTime DateCreated { get; set; }

        public int UpdateBy { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
