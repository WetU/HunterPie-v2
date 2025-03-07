﻿using HunterPie.Core.Client.Localization;
using HunterPie.Core.Domain.Interfaces;
using HunterPie.Core.Extensions;
using HunterPie.Core.Notification;
using HunterPie.Core.Vault;
using HunterPie.Core.Vault.Model;
using HunterPie.Features.Account.Event;
using HunterPie.Features.Account.Model;
using HunterPie.Integrations.Poogie.Account;
using HunterPie.Integrations.Poogie.Account.Models;
using HunterPie.Integrations.Poogie.Common.Models;
using System;
using System.Threading.Tasks;

namespace HunterPie.Features.Account;

internal class AccountManager : IEventDispatcher
{
    private UserAccount? _cachedAccount = null;
    private readonly PoogieAccountConnector _accountConnector = new();
    private static readonly AccountManager Instance = new();

    public static event EventHandler<AccountLoginEventArgs>? OnSignIn;
    public static event EventHandler<EventArgs>? OnSignOut;
    public static event EventHandler<AccountAvatarEventArgs>? OnAvatarChange;

    public static bool IsLoggedIn() => GetSessionToken() is not null;

    public static async Task<bool> ValidateSessionToken()
    {
        if (await FetchAccount() is not null)
            return GetSessionToken() is not null;

        CredentialVaultService.DeleteCredential();
        Instance.Dispatch(OnSignOut);
        return false;

    }

    public static async Task<PoogieResult<LoginResponse>?> Login(LoginRequest request)
    {
        PoogieResult<LoginResponse> loginResponse = await Instance._accountConnector.Login(request);

        if (loginResponse.Error is { } err)
        {
            NotificationService.Error(
                Localization.GetEnumString(err.Code),
                TimeSpan.FromSeconds(10)
            );

            return null;
        }

        if (loginResponse.Response is not { } response)
            return null;

        CredentialVaultService.SaveCredential(request.Email, response.Token);

        UserAccount? account = await FetchAccount();

        if (account is null)
            return null;

        NotificationService.Success(
            Localization.QueryString("//Strings/Client/Integrations/Poogie[@Id='LOGIN_SUCCESS']")
                .Replace("{Username}", account.Username),
            TimeSpan.FromSeconds(5)
        );
        Instance.Dispatch(OnSignIn, new AccountLoginEventArgs { Account = account });

        return loginResponse;
    }

    public static async void Logout()
    {
        _ = await Instance._accountConnector.Logout();

        CredentialVaultService.DeleteCredential();
        Instance._cachedAccount = null;

        Instance.Dispatch(OnSignOut);
    }

    public static string? GetSessionToken()
    {
        Credential? credential = CredentialVaultService.GetCredential();

        return credential?.Password;
    }

    public static async Task UploadAvatar(string path)
    {
        PoogieResult<MyUserAccountResponse> account = await Instance._accountConnector.UploadAvatar(path);

        if (account.Error is { } error)
        {
            NotificationService.Error(
                Localization.GetEnumString(error.Code),
                TimeSpan.FromSeconds(10)
            );
            return;
        }

        NotificationService.Show(
            Localization.QueryString("//Strings/Client/Integrations/Poogie[@Id='AVATAR_UPLOAD_SUCCESS']"),
            TimeSpan.FromSeconds(5)
        );

        Instance._cachedAccount = account.Response!.ToModel();

        Instance.Dispatch(OnAvatarChange, new AccountAvatarEventArgs { AvatarUrl = Instance._cachedAccount.AvatarUrl });
    }

    public static async Task<UserAccount?> FetchAccount()
    {
        Credential? credential = CredentialVaultService.GetCredential();

        if (credential is null)
            return null;

        if (Instance._cachedAccount is { } cached)
            return cached;

        PoogieResult<MyUserAccountResponse> result = await Instance._accountConnector.MyUserAccount();

        if (result.Response is not { } account)
            return null;

        Instance._cachedAccount = account.ToModel();

        return Instance._cachedAccount;
    }
}