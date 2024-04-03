using Microsoft.Extensions.Logging;
using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.ApplicationServices.Operations;
using PasswordManager.Users.Domain.Operations;
using Rebus.Bus;

namespace PasswordManager.Users.ApplicationServices.UserPassword.DeleteUserPassword
{
    internal class DeleteUserPasswordService : IDeleteUserPassword
    {
        private readonly IOperationService _operationService;
        private readonly IPasswordComponent _passwordComponent;
        private readonly ILogger _logger;
        private readonly IBus _bus;

        public DeleteUserPasswordService(IOperationService operationService, IPasswordComponent passwordComponent, ILogger logger, IBus bus)
        {
            _operationService = operationService;
            _passwordComponent = passwordComponent;
            _logger = logger;
            _bus = bus;
        }
        public Task<OperationResult> RequestDeleteUserPassword(Guid userId, OperationDetails operationDetails)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserPassword(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
