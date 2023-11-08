using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Application.Utils
{
    internal class Helpers
    {
        public static string ArrangeValidationErrors(List<ValidationFailure> validationFailures)
        {
            StringBuilder errors = new StringBuilder();

            foreach (var error in validationFailures)
                errors.Append($"{error}\n");

            return errors.ToString();
        }
    }
}
