﻿using PasswordManager.Password.Api.Service.Models;
using PasswordManager.Password.Domain.Password;

<<<<<<<< HEAD:src/Password/Password.Api.Service/Mappers/PasswordResponseMapper.cs
namespace PasswordManager.Password.Api.Service.Mappers;
========
namespace PasswordManager.Password.Api.Service.GetPassword;
>>>>>>>> feature/davu/firebase-user:src/Password/Password.Api.Service/GetPassword/PasswordResponseMapper.cs

internal static class PasswordResponseMapper
{
    internal static PasswordResponse Map(PasswordModel passwordModel)
    {
        var passwordResponse = new PasswordResponse(passwordModel.Id,
                                                    passwordModel.Url,
                                                    passwordModel.FriendlyName,
                                                    passwordModel.Username,
                                                    passwordModel.Password,
                                                    passwordModel.UserId);

        return passwordResponse;
    }

    internal static PasswordResponses Map(IEnumerable<PasswordModel> passwordModels)
    {
        var passwordResponses = passwordModels.Select(Map);

        return new PasswordResponses(passwordModels.Select(Map));
    }
}
