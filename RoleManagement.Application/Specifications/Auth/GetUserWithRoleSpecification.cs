using RoleManagement.Domain.Models;
using RoleManagement.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Application.Specifications.Auth
{
    internal class GetUserWithRoleSpecification : BaseSpecifications<User>
    {
        public GetUserWithRoleSpecification(string Email)
        {
            AddCriteria(x=>x.Email == Email);

            var includes = new List<string>()
            {
                nameof(User.Role)
            };
            AddInclude(includes);
        }
    }
}
