using System.ComponentModel.DataAnnotations;

namespace FINANCE.TRACKER.Models.UserManager
{
    /** 
     * ModuleActionModel
     */
    public class ModuleActionModel
    {
        // The unique identifier for the ModuleActionModel.
        [Key]
        public int ModuleActionId { get; set; }

        // The unique identifier for the ModuleModel.
        public int ModuleId { get; set; }

        // The unique identifier for the ActionModel.
        public int ActionId { get; set; }

        // The unique identifier for the user who created the ModuleActionModel.
        public int CreatedBy { get; set; }

        // The date and time the ModuleActionModel was created.
        public DateTime DateCreated { get; set; }

        // The unique identifier for the user who last updated the ModuleActionModel.
        public int UpdatedBy { get; set; }

        // The date and time the ModuleActionModel was last updated.
        public DateTime DateUpdated { get; set; }
    }
}
