using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.Models.UserManager
{
    public class ModuleActionModel
    {
        [Key]
        public int ModuleActionId { get; set; }

        public int ModuleId { get; set; }

        public int ActionId { get; set; }

        public int CreateBy { get; set; }

        public DateTime DateCreated { get; set; }

        public int UpdateBy { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
