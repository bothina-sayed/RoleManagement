using MapsterMapper;
using Microsoft.Extensions.Logging;
using RoleManagement.Domain.Abstractions;
using RoleManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Application.Services
{
    internal class AuthService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<User> _logger;
        private readonly IGenericRepository<User> _UserRepo;

        public AuthService(IMapper mapper, ILogger<User> logger, IGenericRepository<User> userRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _UserRepo = userRepo;
        }

    }
}
