﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.Models.UserManager
{
    public class ActionModel
    {
        [Key]
        public int ActionId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ActionName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? Description { get; set; }

        public int IsActive { get; set; }

        public int CreateBy { get; set; }

        public DateTime DateCreated { get; set; }

        public int UpdateBy { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
