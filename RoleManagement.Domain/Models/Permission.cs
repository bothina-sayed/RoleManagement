using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleManagement.Domain.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [ForeignKey("PermissionId")]
        [InverseProperty("Permissions")]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
