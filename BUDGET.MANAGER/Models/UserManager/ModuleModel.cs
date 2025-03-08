using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.Models.UserManager
{
    public class ModuleModel
    {
        [Key]
        public int ModuleId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ModuleName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? Description { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ModulePage { get; set; }

        public int IsActive { get; set; }

        public int CreateBy { get; set; }

        public DateTime DateCreated { get; set; }

        public int UpdateBy { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
