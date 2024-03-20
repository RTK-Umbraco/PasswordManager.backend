using PasswordManager.Password.Domain.Password;

namespace PasswordManager.Password.Api.Service.Endpoints.GetPassword;

internal static class PasswordResponseMapper
{
    internal static PasswordResponse Map(PasswordModel passwordModel)
    {
        var passwordResponse = new PasswordResponse(passwordModel.Id,
                                                    passwordModel.Url,
                                                    passwordModel.FriendlyName,
                                                    passwordModel.Username,
                                                    passwordModel.Password);

        return passwordResponse;
    }

    internal static IEnumerable<PasswordResponse> Map(IEnumerable<PasswordModel> passwordModels)
    {
        return passwordModels.Select(Map);
    }
}
