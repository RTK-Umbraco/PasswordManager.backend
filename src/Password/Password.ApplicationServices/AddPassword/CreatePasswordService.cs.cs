using Microsoft.Extensions.Logging;
using PasswordManager.Password.ApplicationServices.Operations;
using PasswordManager.Password.ApplicationServices.Repositories.Operations;
using PasswordManager.Password.ApplicationServices.Repositories.Password;
using PasswordManager.Password.Domain.Password;
using Rebus.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Password.ApplicationServices.AddPassword
{
    public class CreatePasswordService : ICreatePasswordService
    {
        private readonly Operations.IOperationService _operationService;
        private readonly IBus _bus;
        private readonly ILogger<CreatePasswordService> _logger;
        private readonly IPasswordRepository _passwordRepository;

        public CreatePasswordService(Operations.IOperationService operationService, IBus bus, ILogger<CreatePasswordService> logger, IPasswordRepository passwordRepository)
        {
            _operationService = operationService;
            _bus = bus;
            _logger = logger;
            _passwordRepository = passwordRepository;
        }

        public async Task<PasswordModel> CreatePassword()
        {
            _logger.LogInformation("Adding password");

            return null;
        }
    }
}
