// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Moq;
using Open.IdentityServer.Stores.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using ISModels = Open.IdentityServer.Models;

namespace Aguacongas.Open.IdentityServer.Http.Store.Test;

public class AuthorizationCodeStoreTest
{
    [Fact]
    public async Task GetAuthorizationCodeAsync_should_call_store_GetAsync()
    {
        CreateSut(out Mock<IAdminStore<AuthorizationCode>> storeMock,
            out AuthorizationCodeStore sut);

        storeMock.Setup(m => m.GetAsync(It.IsAny<string>(), null))
            .ReturnsAsync(new AuthorizationCode())
            .Verifiable();

        await sut.GetAuthorizationCodeAsync("test");

        storeMock.Verify(m => m.GetAsync("test", null));
    }

    [Fact]
    public async Task RemoveAuthorizationCodeAsync_should_call_store_DeleteAsync()
    {
        CreateSut(out Mock<IAdminStore<AuthorizationCode>> storeMock,
            out AuthorizationCodeStore sut);

        storeMock.Setup(m => m.DeleteAsync(It.IsAny<string>())).Verifiable();
        storeMock.Setup(m => m.GetAsync(It.IsAny<string>(), It.IsAny<GetRequest>()))
            .ReturnsAsync(new AuthorizationCode
            {
                Id = "id"
            })
            .Verifiable();

        await sut.RemoveAuthorizationCodeAsync("test");

        storeMock.Verify(m => m.GetAsync("test", null));
        storeMock.Verify(m => m.DeleteAsync(It.Is<string>(r => r == "id")));
    }

    [Fact]
    public async Task StoreAuthorizationCodeAsync_should_call_store_CreateAsync()
    {
        CreateSut(out Mock<IAdminStore<AuthorizationCode>> storeMock,
            out AuthorizationCodeStore sut);

        storeMock.Setup(m => m.CreateAsync(It.IsAny<AuthorizationCode>()))
            .ReturnsAsync(new AuthorizationCode())
            .Verifiable();
        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<AuthorizationCode>
            {
                Count = 0,
                Items = []
            })
            .Verifiable();
        await sut.StoreAuthorizationCodeAsync(new ISModels.AuthorizationCode
        {
            ClientId = "test",
            Subject = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, "test")]))
        });

        storeMock.Verify(m => m.CreateAsync(It.IsAny<AuthorizationCode>()));
    }

    private static void CreateSut(out Mock<IAdminStore<AuthorizationCode>> storeMock,
        out AuthorizationCodeStore sut)
    {
        storeMock = new Mock<IAdminStore<AuthorizationCode>>();
        var serializerMock = new Mock<IPersistentGrantSerializer>();
        sut = new AuthorizationCodeStore(storeMock.Object, serializerMock.Object);
    }
}