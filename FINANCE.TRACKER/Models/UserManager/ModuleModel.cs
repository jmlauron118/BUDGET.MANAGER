using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FINANCE.TRACKER.Models.UserManager
{
    /** 
     * ModuleModel
     */
    public class ModuleModel
    {
        // The unique identifier for the ModuleModel.
        [Key]
        public int ModuleId { get; set; }

        // The name of the module.
        [Column(TypeName = "nvarchar(50)")]
        public string? ModuleName { get; set; }

        // The description of the module.
        [Column(TypeName = "nvarchar(150)")]
        public string? Description { get; set; }

        // The page associated with the module.
        [Column(TypeName = "nvarchar(50)")]
        public string? ModulePage { get; set; }

        // The icon associated with the module.
        public string? Icon { get; set; }

        // The status of the module.
        public int IsActive { get; set; }

        // The sort number of the module.
        public int SortNo { get; set; }

        // The unique identifier for the user who created the ModuleModel.
        public int CreatedBy { get; set; }

        // The date and time the ModuleModel was created.
        public DateTime DateCreated { get; set; }

        // The unique identifier for the user who last updated the ModuleModel.
        public int UpdatedBy { get; set; }

        // The date and time the ModuleModel was last updated.
        public DateTime DateUpdated { get; set; }
    }
}
