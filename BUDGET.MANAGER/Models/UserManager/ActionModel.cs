using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.Models.UserManager
{
    /** 
     * ActionModel
     */
    public class ActionModel
    {
        // ActionId property
        [Key]
        public int ActionId { get; set; }

        // ModuleId property
        [Column(TypeName = "nvarchar(50)")]
        public string? ActionName { get; set; }

        // Description property
        [Column(TypeName = "nvarchar(150)")]
        public string? Description { get; set; }

        // IsActive property
        public int IsActive { get; set; }

        // CreatedBy property
        public int CreatedBy { get; set; }

        // DateCreated property
        public DateTime DateCreated { get; set; }

        // UpdatedBy property
        public int UpdatedBy { get; set; }

        // DateUpdated property
        public DateTime DateUpdated { get; set; }
    }
}
