using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUDGET.MANAGER.Models.UserManager
{
    /**
     * UserModel
     */
    public class UserModel
    {
        // The unique identifier for the user.
        [Key]
        public int UserId { get; set; }

        // The first name of the user.
        [Column(TypeName = "nvarchar(50)")]
        public string? Firstname { get; set; }

        // The last name of the user.
        [Column(TypeName = "nvarchar(50)")]
        public string? Lastname { get; set; }

        // The gender of the user.
        [Column(TypeName = "nvarchar(10)")]
        public string? Gender { get; set; }

        // The username of the user.
        [Column(TypeName = "nvarchar(50)")]
        public string? Username { get; set; }

        // The password of the user.
        [Column(TypeName = "nvarchar(50)")]
        public string? Password { get; set; }

        //The status of the user.
        public int IsActive { get; set; }

        // The unique identifier for the user who created the user.
        public int CreatedBy { get; set; }

        // The date and time the user was created.
        public DateTime DateCreated { get; set; }

        // The unique identifier for the user who last updated the user.
        public int UpdatedBy { get; set; }

        // The date and time the user was last updated.
        public DateTime DateUpdated { get; set; }
    }
}
