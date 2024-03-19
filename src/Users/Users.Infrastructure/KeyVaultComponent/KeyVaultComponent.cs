using Microsoft.Extensions.Logging;
using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.Domain.User;
using Umbraco.Cloud.Passwordmanager.Keyvaults.Api.Client;

namespace PasswordManager.Users.Infrastructure.KeyVaultComponent;
public sealed class KeyVaultComponent : IKeyVaultComponent
{
    private readonly IPasswordmanagerKeyvaultsApiClient _passwordmanagerKeyvaultsApiClient;
    private ILogger<KeyVaultComponent> _logger;

    public KeyVaultComponent(IPasswordmanagerKeyvaultsApiClient passwordmanagerKeyvaultsApiClient, ILogger<KeyVaultComponent> logger)
    {
        _passwordmanagerKeyvaultsApiClient = passwordmanagerKeyvaultsApiClient;
        _logger = logger;
    }

    public async Task<string> CreateEncryptedPassword(UserPasswordModel userPasswordModel)
    {
        try
        {
            _logger.LogInformation("Requesting to create password");
            var encryptedPassword = await _passwordmanagerKeyvaultsApiClient.ProtectTextAsync(userPasswordModel.UserId.ToString(), 
                new ProtectTextRequestDetails(userPasswordModel.UserId, userPasswordModel.Password));

            var encryptedPasswordResult = encryptedPassword.Result;

            return encryptedPasswordResult.ProtectedText;

        }
        catch (ApiException exception)
        {
            _logger.LogError(exception, "Could not request password, service returned {StatusCode} and message {ErrorMessage}",
               exception.StatusCode, exception.Message);

            throw new PasswordComponentException("Error calling PasswordApiClient.CreatePasswordAsync", exception);
        }
    }
}
