using PasswordManager.Password.Domain.Password;

namespace PasswordManager.Password.Api.Service.GetPassword;

public static class PasswordResponseMapper
{
    public static PasswordResponse Map(PasswordModel passwordModel)
    {
        var passwordResponse = new PasswordResponse(passwordModel.Id,
                                                    passwordModel.Url,
                                                    passwordModel.Label,
                                                    passwordModel.Username,
                                                    passwordModel.Key);

        return passwordResponse;
    }

    public static IEnumerable<PasswordResponse> Map(IEnumerable<PasswordModel> passwordModels)
    {
        return passwordModels.Select(Map);
    }
}
