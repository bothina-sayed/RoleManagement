using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Domain.ViewModels
{
    public class CityViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
