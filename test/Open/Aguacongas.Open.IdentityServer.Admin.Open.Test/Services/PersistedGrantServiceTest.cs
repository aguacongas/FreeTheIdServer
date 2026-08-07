// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Admin.Services;
using Aguacongas.Open.IdentityServer.Store;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using Open.IdentityServer.Models;
using Open.IdentityServer.Stores.Serialization;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.Open.IdentityServer.Admin.Test.Services;

public class PersistedGrantServiceTest
{
    [Fact]
    public async Task RemoveAllGrantsAsync_should_return_user_grants()
    {
        var sut = CreateSut(out Mock<IDataProtector> _);

        await sut.RemoveAllGrantsAsync("test", "test", "test");

        await sut.RemoveAllGrantsAsync("test", "test");

        await sut.RemoveAllGrantsAsync("test");

        var grants = await sut.GetAllGrantsAsync("test");

        Assert.NotEmpty(grants);
    }

    [Fact]
    public async Task GetAllGrantsAsync_should_catch_decryption_error()
    {
        var sut = CreateSut(out Mock<IDataProtector> mock);

        mock.Setup(m => m.Unprotect(It.IsAny<byte[]>())).Throws(new CryptographicException());
        var grants = await sut.GetAllGrantsAsync("test");

        Assert.NotEmpty(grants);
    }

    private static PersistedGrantService CreateSut(out Mock<IDataProtector> mock)
    {
        var authorizationCodeStoreMock = new Mock<IAdminStore<Entity.AuthorizationCode>>();
        var userConsentStoreMock = new Mock<IAdminStore<Entity.UserConsent>>();
        var refreshTokenStoreMock = new Mock<IAdminStore<Entity.RefreshToken>>();
        var referenceTokenStoreMock = new Mock<IAdminStore<Entity.ReferenceToken>>();
        var dataProtectorProviderMock = new Mock<IDataProtectionProvider>();
        var dataProtectorMock = new Mock<IDataProtector>();
        dataProtectorMock.Setup(m => m.Protect(It.IsAny<byte[]>())).Returns<byte[]>(b => b);
        dataProtectorMock.Setup(m => m.Unprotect(It.IsAny<byte[]>())).Returns<byte[]>(b => b);
        dataProtectorProviderMock.Setup(m => m.CreateProtector(It.IsAny<string>())).Returns(dataProtectorMock.Object);

        var serializer = new PersistentGrantSerializer();
        var localizerMock = new Mock<IStringLocalizer<PersistedGrantService>>();
        var loggerMock = new Mock<ILogger<PersistedGrantService>>();

        var sut = new PersistedGrantService(authorizationCodeStoreMock.Object,
            userConsentStoreMock.Object,
            refreshTokenStoreMock.Object,
            referenceTokenStoreMock.Object,
            serializer,
            localizerMock.Object,
            loggerMock.Object);

        authorizationCodeStoreMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>(), default)).ReturnsAsync(new PageResponse<Entity.AuthorizationCode>
        {
            Items = [
            new Entity.AuthorizationCode
            {
                Data = serializer.Serialize(new AuthorizationCode{
                    RequestedScopes = []
                })
            }
            ]
        });
        userConsentStoreMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>(), default)).ReturnsAsync(new PageResponse<Entity.UserConsent>
        {
            Items = [ new Entity.UserConsent
            {
                Data = serializer.Serialize(new Consent{
                    Scopes = []
                })
            } ]
        });
        var refreshToken = new RefreshToken();
        refreshToken.AccessTokens.Add("empty", new Token
        {
            Claims = Array.Empty<Claim>()
        });
        refreshTokenStoreMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>(), default)).ReturnsAsync(new PageResponse<Entity.RefreshToken>
        {
            Items = [ new Entity.RefreshToken(){
                Data = serializer.Serialize(refreshToken)
            } ]
        });
        referenceTokenStoreMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>(), default)).ReturnsAsync(new PageResponse<Entity.ReferenceToken>
        {
            Items = [ new Entity.ReferenceToken(){
                Data = serializer.Serialize(new Token{
                    Claims = Array.Empty<Claim>()
                })
            } ]
        });

        mock = dataProtectorMock;
        return sut;
    }
}