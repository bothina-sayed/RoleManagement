using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Domain.ViewModels.Employee
{
    internal class CreateEmployeeViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string NationalId { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Region { get; set; }
        [Required]
        public string? District { get; set; }
        [Required]
        public decimal? Salary { get; set; }
    }
}
