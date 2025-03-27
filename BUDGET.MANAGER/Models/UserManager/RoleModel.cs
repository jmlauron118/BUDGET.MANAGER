using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.Models.UserManager
{
    /**
     * RoleModel
     */
    public class RoleModel
    {
        // The unique identifier for the RoleModel.
        [Key]
        public int RoleId { get; set; }

        // The name of the role.
        public string? Role { get; set; }

        //  The description of the role.
        public string? Description { get; set; }

        // The status of the role.
        public int IsActive { get; set; }

        // The unique identifier for the user who created the RoleModel.
        public int CreatedBy { get; set; }

        // The date and time the RoleModel was created.
        public DateTime DateCreated { get; set; }

        // The unique identifier for the user who last updated the RoleModel.
        public int UpdatedBy { get; set; }

        // The date and time the RoleModel was last updated.
        public DateTime DateUpdated { get; set; }
    }
}
