using PasswordManager.Password.Domain.Password;

namespace PasswordManager.Password.Api.Service.GetPassword;

public static class PasswordResponseMapper
{
    public static PasswordResponse Map(PasswordModel passwordModel)
    {
        var passwordResponse = new PasswordResponse(passwordModel.Id,
                                                    passwordModel.Url,
                                                    passwordModel.FriendlyName,
                                                    passwordModel.Username,
                                                    passwordModel.Password);

        return passwordResponse;
    }
}
