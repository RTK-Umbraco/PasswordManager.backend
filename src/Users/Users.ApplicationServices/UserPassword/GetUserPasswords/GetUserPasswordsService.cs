﻿using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.ApplicationServices.Repositories.User;
using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.UserPassword.GetUserPasswords;
public sealed class GetUserPasswordsService : IGetUserPasswordsService
{
    private readonly IPasswordComponent _passwordComponent;
    private readonly IKeyVaultComponent _KeyVaultComponent;
    private readonly IUserRepository _userRepository;

    public GetUserPasswordsService(IPasswordComponent passwordComponent, IUserRepository userRepository, IKeyVaultComponent keyVaultComponent)
    {
        _passwordComponent = passwordComponent;
        _userRepository = userRepository;
        _KeyVaultComponent = keyVaultComponent;
    }

    public async Task<IEnumerable<UserPasswordModel>> GetUserPasswords(Guid userId)
    {
        var user = await _userRepository.Get(userId) ?? throw new GetUserPasswordsServiceException("Could not find user");

        if (user.IsDeleted())
        {
            throw new GetUserPasswordsServiceException("Cannot get user password because the user is marked as deleted");
        }

        var encryptedPasswords = await _passwordComponent.GetUserPasswords(userId);

        var decryptedPassword = await _KeyVaultComponent.DecryptPasswords(encryptedPasswords, user.SecretKey);

        return decryptedPassword;
    }

    public async Task<IEnumerable<UserPasswordModel>> GetUserPasswordsByUrl(Guid userId, string url)
    {
        var user = await _userRepository.Get(userId) ?? throw new GetUserPasswordsServiceException("Could not find user");

        if (user.IsDeleted())
        {
            throw new GetUserPasswordsServiceException("Cannot get user password because the user is marked as deleted");
        }

        var encryptedPasswords = await _passwordComponent.GetUserPasswordsFromUrl(userId, url);

        var decryptedPassword = await _KeyVaultComponent.DecryptPasswords(encryptedPasswords, user.SecretKey);

        return decryptedPassword;
    }
}
