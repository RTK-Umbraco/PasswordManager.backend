using Microsoft.Extensions.Logging;
using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.Domain.User;
using System.Reflection.Metadata.Ecma335;
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

    public async Task<string> CreateEncryptedPassword(UserPasswordModel userPasswordModel, string secretKey)
    {
        try
        {
            _logger.LogInformation("Requesting to create password");
            var encryptedPassword = await _passwordmanagerKeyvaultsApiClient.ProtectItemAsync(
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

    public async Task<IEnumerable<UserPasswordModel>> DecryptPasswords(IEnumerable<UserPasswordModel> userPasswordModels, string secretKey)
    {
        var items = new List<Item>(userPasswordModels.Select(user => new Item(user.PasswordId, user.Password)));
        var decryptedPasswords = await _passwordmanagerKeyvaultsApiClient.UnprotectItemAsync(new UnprotectItemRequestDetails(items, secretKey));

        var decryptedPasswordsResult = decryptedPasswords.Result;

        List<UserPasswordModel> decryptedUserPasswords = new List<UserPasswordModel>();

        foreach (var user in userPasswordModels)
        {
            foreach (var decryptedPassword in decryptedPasswordsResult.ProtectedItems)
            {
                if (decryptedPassword.ItemId == user.PasswordId)
                {
                    user.SetEncryptedPassword(decryptedPassword.UnprotectedItem);
                }
            }
            decryptedUserPasswords.Add(user);
        }

        return decryptedUserPasswords;
    }
}
