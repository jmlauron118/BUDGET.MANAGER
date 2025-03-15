using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUDGET.MANAGER.Models.UserManager
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Firstname { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Lastname { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? Gender { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Username { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Password { get; set; }

        public int IsActive { get; set; }

        public int CreateBy { get; set; }

        public DateTime DateCreated { get; set; }

        public int UpdateBy { get; set; }

        public int DateUpdated { get; set; }
    }
}
