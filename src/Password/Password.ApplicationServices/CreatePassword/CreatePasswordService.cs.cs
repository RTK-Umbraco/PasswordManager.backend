using PasswordManager.Password.ApplicationServices.Repositories.Password;
using PasswordManager.Password.ApplicationServices.Operations;

namespace PasswordManager.Password.ApplicationServices.CreatePassword;
public sealed class CreatePasswordService : ICreatePasswordService
{
    private readonly IOperationService _operationService;
    private readonly IBus _bus;
    private readonly ILogger<CreatePasswordService> _logger;
    private readonly IPasswordRepository _passwordRepository;

    public CreatePasswordService(IOperationService operationService, IBus bus, ILogger<CreatePasswordService> logger, IPasswordRepository passwordRepository)
    {
        _operationService = operationService;
        _bus = bus;
        _logger = logger;
        _passwordRepository = passwordRepository;
    }

    public async Task<OperationResult> RequestCreatePassword(PasswordModel passwordModel, OperationDetails operationDetails)
    {
        _logger.LogInformation($"Request creating password {passwordModel.Id}");

        //Encrypt password here. The reason why is that we don't want to store the plain text password in the operation table. 
        var operation = await _operationService.QueueOperation(OperationBuilder.CreatePassword(passwordModel, operationDetails.CreatedBy));

        await _bus.Send(new CreatePasswordCommand(operation.RequestId));

        _logger.LogInformation($"Request sent to worker for creating password: {passwordModel.Id} - requestId: {operation.RequestId}");

        return OperationResult.Accepted(operation);
    }

    public async Task CreatePassword(PasswordModel passwordModel)
    {
        _logger.LogInformation($"Creating password: {passwordModel.Id}");

        var password = new PasswordModel(passwordModel.Id, passwordModel.Url, passwordModel.FriendlyName, passwordModel.Username, passwordModel.Password);

        await _passwordRepository.Add(password);

        _logger.LogInformation($"Password created: {passwordModel.Id}");

        return;
    }
}
