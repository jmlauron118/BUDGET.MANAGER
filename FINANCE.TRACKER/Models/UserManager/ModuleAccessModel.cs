using System.ComponentModel.DataAnnotations;

namespace FINANCE.TRACKER.Models.UserManager
{
    /** 
     * ModuleAccessModel
     */
    public class ModuleAccessModel
    {
        // The unique identifier for the ModuleAccessModel.
        [Key]
        public int ModuleAccessId { get; set; }

        // The unique identifier for the ModuleActionModel.
        public int ModuleActionId { get; set; }

        // The unique identifier for the UserRoleModel.
        public int UserRoleId { get; set; }

        // The unique identifier for the user who created the ModuleAccessModel.
        public int CreatedBy { get; set; }

        // The date and time the ModuleAccessModel was created.
        public DateTime DateCreated { get; set; }

        // The unique identifier for the user who last updated the ModuleAccessModel.
        public int UpdatedBy { get; set; }

        // The date and time the ModuleAccessModel was last updated.
        public DateTime DateUpdated { get; set; }
    }
}
