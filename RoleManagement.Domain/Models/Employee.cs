using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleManagement.Domain.Models
{
    public partial class Employee
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Username { get; set; } = null!;
        [StringLength(100)]
        public string FullName { get; set; } = null!;
        [Column("National_Id")]
        [StringLength(20)]
        public string NationalId { get; set; } = null!;
        [StringLength(50)]
        public string? City { get; set; }
        [StringLength(50)]
        public string? Region { get; set; }
        [StringLength(50)]
        public string? District { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Salary { get; set; }
    }
}
