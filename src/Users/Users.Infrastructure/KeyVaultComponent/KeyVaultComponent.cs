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

    public async Task<string> CreateEncryptedPassword(UserPasswordModel userPasswordModel, Guid secretKey)
    {
        try
        {
            _logger.LogInformation("Requesting to create password");
            var encryptedPassword = await _passwordmanagerKeyvaultsApiClient.ProtectItemAsync(userPasswordModel.UserId.ToString(), 
                new ProtectItemRequestDetails(userPasswordModel.Password, secretKey));

            var encryptedPasswordResult = encryptedPassword.Result;

            return encryptedPasswordResult.ProtectedItem;

        }
        catch (ApiException exception)
        {
            _logger.LogError(exception, "Could not encrypt password, service returned {StatusCode} and message {ErrorMessage}",
               exception.StatusCode, exception.Message);

            throw new KeyVaultComponentException("Error calling KeyVaultApiClient.ProtectItemAsync", exception);
        }
    }
}
