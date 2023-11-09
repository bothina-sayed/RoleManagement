using FluentValidation;
using RoleManagement.Application.Utils;
using RoleManagement.Domain.Abstractions;
using RoleManagement.Domain.Models;
using RoleManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Application.Validations.Auth
{
    internal class LoginValidation : AbstractValidator<LoginViewModel>
    {
        private readonly IGenericRepository<User> _userRepo;

        public LoginValidation(IGenericRepository<User> userRepo)
        {
            _userRepo = userRepo;

            RuleFor(u => new { u.Email, u.Password })
                .NotEmpty().MustAsync((a, cancellationToken) => isEmailExist(a.Email, a.Password, cancellationToken)).WithMessage("Wrong Email or Password");
        }
        private async Task<bool> isEmailExist(string email, string password, CancellationToken cancellationToken)
        {
            var customer = await _userRepo.GetObj(x => x.Email == email);

            var pass = Cipher.Decrypt(customer.Password, customer.EncryptionKey);

            if (customer == null ||
                (customer != null && pass != password))
                return false;
            return true;

        }
    }
}
