using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleManagement.Domain.Models
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Email { get; set; } = null!;
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [MaxLength(255)]
        public byte[] Password { get; set; } = null!;
        [MaxLength(255)]
        public byte[] EncryptionKey { get; set; } = null!;
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("Users")]
        public virtual Role? Role { get; set; }
    }
}
