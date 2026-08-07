// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Moq;
using Open.IdentityServer.Stores.Serialization;
using System.Threading.Tasks;
using Xunit;
using ISModels = Open.IdentityServer.Models;

namespace Aguacongas.Open.IdentityServer.Http.Store.Test;

public class RefreshTokenStoreTest
{
    [Fact]
    public async Task GetRefreshTokenAsync_should_call_store_GetAsync()
    {
        CreateSut(out Mock<IAdminStore<RefreshToken>> storeMock,
            out RefreshTokenStore sut);

        storeMock.Setup(m => m.GetAsync(It.IsAny<string>(), It.IsAny<GetRequest>()))
            .ReturnsAsync(new RefreshToken())
            .Verifiable();

        await sut.GetRefreshTokenAsync("test");

        storeMock.Verify(m => m.GetAsync("test", null));
    }

    [Fact]
    public async Task RemoveRefreshTokenAsync_should_call_store_DeleteAsync()
    {
        CreateSut(out Mock<IAdminStore<RefreshToken>> storeMock,
            out RefreshTokenStore sut);

        storeMock.Setup(m => m.DeleteAsync(It.IsAny<string>())).Verifiable();

        storeMock.Setup(m => m.GetAsync(It.IsAny<string>(), It.IsAny<GetRequest>()))
            .ReturnsAsync(new RefreshToken
            {
                Id = "id"
            })
            .Verifiable();

        await sut.RemoveRefreshTokenAsync("test");

        storeMock.Verify(m => m.GetAsync("test", null));
        storeMock.Verify(m => m.DeleteAsync(It.Is<string>(r => r == "id")));
    }

    [Fact]
    public async Task RemoveRefreshTokensAsync_should_call_store_DeleteAsync()
    {
        CreateSut(out Mock<IAdminStore<RefreshToken>> storeMock,
            out RefreshTokenStore sut);

        storeMock.Setup(m => m.DeleteAsync(It.IsAny<string>())).Verifiable();

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<RefreshToken>
            {
                Count = 1,
                Items =
                [
                    new RefreshToken
                    {
                        Id = "id"
                    }
                ]
            })
            .Verifiable();

        await sut.RemoveRefreshTokensAsync("test", "test");

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p => p.Filter == "UserId eq 'test' and ClientId eq 'test'")));
        storeMock.Verify(m => m.DeleteAsync(It.Is<string>(r => r == "id")));
    }

    [Fact]
    public async Task StoreRefreshTokenAsync_should_call_store_CreateAsync()
    {
        CreateSut(out Mock<IAdminStore<RefreshToken>> storeMock,
            out RefreshTokenStore sut);

        storeMock.Setup(m => m.CreateAsync(It.IsAny<RefreshToken>()))
            .ReturnsAsync(new RefreshToken())
            .Verifiable();

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<RefreshToken>
            {
                Count = 0,
                Items = []
            })
            .Verifiable();
        var refreshToken = new ISModels.RefreshToken();
        refreshToken.AccessTokens.Add("test",
            new ISModels.Token
            {
                ClientId = "test"
            }
        );
        await sut.StoreRefreshTokenAsync(refreshToken);

        storeMock.Verify(m => m.GetAsync(It.IsAny<PageRequest>()));
        storeMock.Verify(m => m.CreateAsync(It.IsAny<RefreshToken>()));
    }

    [Fact]
    public async Task UpdateRefreshTokenAsync_should_call_store_UpdateAsync()
    {
        CreateSut(out Mock<IAdminStore<RefreshToken>> storeMock,
            out RefreshTokenStore sut);

        storeMock.Setup(m => m.UpdateAsync(It.IsAny<RefreshToken>()))
            .ReturnsAsync(new RefreshToken())
            .Verifiable();

        storeMock.Setup(m => m.GetAsync(It.IsAny<string>(), It.IsAny<GetRequest>()))
            .ReturnsAsync(new RefreshToken())
            .Verifiable();
        var refreshToken = new ISModels.RefreshToken();
        refreshToken.AccessTokens.Add("test",
            new ISModels.Token
            {
                ClientId = "test"
            }
        );
        await sut.UpdateRefreshTokenAsync("test", refreshToken);

        storeMock.Verify(m => m.GetAsync("test", null));
        storeMock.Verify(m => m.UpdateAsync(It.IsAny<RefreshToken>()));
    }

    private static void CreateSut(out Mock<IAdminStore<RefreshToken>> storeMock,
        out RefreshTokenStore sut)
    {
        storeMock = new Mock<IAdminStore<RefreshToken>>();
        var serializerMock = new Mock<IPersistentGrantSerializer>();
        sut = new RefreshTokenStore(storeMock.Object, serializerMock.Object);
    }
}