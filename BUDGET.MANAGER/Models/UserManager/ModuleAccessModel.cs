using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.Models.UserManager
{
    public class ModuleAccessModel
    {
        [Key]
        public int ModuleAccessId { get; set; }

        public int ModuleActionId { get; set; }

        public int UserRoleId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
