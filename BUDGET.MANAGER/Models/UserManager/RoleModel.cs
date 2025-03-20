using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.Models.UserManager
{
    public class RoleModel
    {
        [Key]
        public int RoleId { get; set; }

        public string? Role { get; set; }

        public string? Description { get; set; }

        public int IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
