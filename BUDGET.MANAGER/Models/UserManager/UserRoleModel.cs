using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.Models.UserManager
{
    /**
     * UserRoleModel
     */
    public class UserRoleModel
    {
        // The unique identifier for the user role.
        [Key]
        public int UserRoleId { get; set; }

        // The unique identifier for the user.
        public int UserId { get; set; }

        // The unique identifier for the role.
        public int RoleId { get; set; }

        // The unique identifier for the user who created the user role.
        public int CreatedBy { get; set; }

        // The date and time the user role was created.
        public DateTime DateCreated { get; set; }

        // The unique identifier for the user who last updated the user role.
        public int UpdatedBy { get; set; }

        // The date and time the user role was last updated.
        public DateTime DateUpdated { get; set; }
    }
}
