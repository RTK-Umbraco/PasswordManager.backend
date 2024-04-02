using Microsoft.Extensions.Logging;
using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.Domain.User;
using Umbraco.Cloud.Passwordmanager.Password.Api.Client;

namespace PasswordManager.Users.Infrastructure.PasswordComponent;
public sealed class PasswordComponent : IPasswordComponent
{
    private readonly IPasswordmanagerPasswordApiClient _passwordmanagerPasswordApiClient;
    private ILogger<PasswordComponent> _logger;

    public PasswordComponent(IPasswordmanagerPasswordApiClient passwordmanagerPasswordApiClient, ILogger<PasswordComponent> logger)
    {
        _passwordmanagerPasswordApiClient = passwordmanagerPasswordApiClient;
        _logger = logger;
    }

    public async Task CreateUserPassword(UserPasswordModel userPasswordModel)
    {
        try
        {
            _logger.LogInformation("Requesting to create password");
            await _passwordmanagerPasswordApiClient.CreatePasswordAsync(new CreatePasswordRequestWithBody(userPasswordModel.UserId.ToString(),
                new CreatePasswordRequestDetails(userPasswordModel.FriendlyName, userPasswordModel.Password, userPasswordModel.Url, userPasswordModel.UserId, userPasswordModel.Username)));
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
            var passwordsResponse = await _passwordmanagerPasswordApiClient.GetPasswordsByUserIdAndUrlAsync(new GetPasswordByUserIdAndUrlRequestDetails(url, userId));

            var passwordsResponseResult = passwordsResponse.Result;

            return passwordsResponseResult.PasswordsResponses.Select(UserPasswordModelMapper.Map);
        }
        catch (ApiException exception)
        {
            throw new PasswordComponentException("Error calling PasswordApiClient.GetPasswordAsync", exception);
        }
    }
}
