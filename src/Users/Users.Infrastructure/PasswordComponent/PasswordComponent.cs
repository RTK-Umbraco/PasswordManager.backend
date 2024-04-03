using Microsoft.Extensions.Logging;
using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.Domain.User;
using Umbraco.Cloud.Passwordmanager.Password.Api.Client;

namespace PasswordManager.Users.Infrastructure.PasswordComponent;
public sealed class PasswordComponent : IPasswordComponent
{
    private readonly IPasswordmanagerPasswordApiClient _passwordmanagerPasswordApiClient;
    private readonly ILogger<PasswordComponent> _logger;

    public PasswordComponent(IPasswordmanagerPasswordApiClient passwordmanagerPasswordApiClient, ILogger<PasswordComponent> logger)
    {
        _passwordmanagerPasswordApiClient = passwordmanagerPasswordApiClient;
        _logger = logger;
    }

    public async Task CreateUserPassword(UserPasswordModel userPasswordModel, string createdByUserId)
    {
        try
        {
            _logger.LogInformation("Requesting to create password");
            await _passwordmanagerPasswordApiClient.CreatePasswordAsync(new CreatePasswordRequestWithBody(
                createdByUserId,
                new CreatePasswordRequestDetails(
                    userPasswordModel.FriendlyName, 
                    userPasswordModel.Password, 
                    userPasswordModel.Url, 
                    userPasswordModel.UserId, 
                    userPasswordModel.Username
                    )));
        }
        catch (ApiException exception)
        {
            _logger.LogError(exception, "Could not request password, service returned {StatusCode} and message {ErrorMessage}",
               exception.StatusCode, exception.Message);

            throw new PasswordComponentException("Error calling PasswordApiClient.CreatePasswordAsync", exception);
        }
    }

    public async Task<IEnumerable<UserPasswordModel>> GetUserPasswords(Guid userId)
    {
        try
        {
            var passwordsResponse = await _passwordmanagerPasswordApiClient.GetPasswordsFromUserIdAsync(userId);

            var passwordsResponseResult = passwordsResponse.Result;

            return passwordsResponseResult.PasswordsResponses.Select(UserPasswordModelMapper.Map);
        }catch(ApiException exception)
        {
            throw new PasswordComponentException("Error calling PasswordApiClient.GetPasswordAsync", exception);
        }
    }

    public async  Task<IEnumerable<UserPasswordModel>> GetUserPasswordsFromUrl(Guid userId, string url)
    {
        try
        {
            var passwordsResponse = await _passwordmanagerPasswordApiClient.GetPasswordsByUserIdAndUrlAsync(userId, url);

            var passwordsResponseResult = passwordsResponse.Result;

            return passwordsResponseResult.PasswordsResponses.Select(UserPasswordModelMapper.Map);
        }
        catch (ApiException exception)
        {
            throw new PasswordComponentException("Error calling PasswordApiClient.GetPasswordAsync", exception);
        }
    }

    public async Task UpdateUserPassword(UserPasswordModel userPasswordModel, string createdByUserId)
    {
        try
        {
            var updatePasswordRequest = await _passwordmanagerPasswordApiClient.UpdatePasswordAsync(
                userPasswordModel.PasswordId, 
                createdByUserId, 
                new UpdatePasswordRequestDetails(
                    userPasswordModel.FriendlyName, 
                    userPasswordModel.Password, 
                    userPasswordModel.Url,
                    userPasswordModel.Username
                ));
        }
        catch(ApiException exception)
        {
            throw new PasswordComponentException("Error calling PasswordApiClient.UpdatePasswordAsync", exception);
        }
    }

    public async Task DeleteUserPassword(Guid passwordId, string createdByUserId)
    {
        try
        {
            var deletePasswordRequest = await _passwordmanagerPasswordApiClient.DeletePasswordAsync(passwordId, createdByUserId);
        }
        catch (ApiException exception)
        {
            throw new PasswordComponentException("Error calling PasswordApiClient.UpdatePasswordAsync", exception);
        }
    }

    public async Task<string> GenerateUserPassword(int length)
    {
        try
        {
            var generatePasswordRequest = await _passwordmanagerPasswordApiClient.GeneratePasswordAsync(length);
            return generatePasswordRequest.Result.Password;
        }
        catch (ApiException exception)
        {
            throw new PasswordComponentException("Error calling PasswordApiClient.UpdatePasswordAsync", exception);
        }
    }
}
