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

public class ReferenceTokenStoreTest
{
    [Fact]
    public async Task GetReferenceTokenAsync_should_call_store_GetAsync()
    {
        CreateSut(out Mock<IAdminStore<ReferenceToken>> storeMock,
            out ReferenceTokenStore sut);

        storeMock.Setup(m => m.GetAsync(It.IsAny<string>(), It.IsAny<GetRequest>()))
            .ReturnsAsync(new ReferenceToken())
            .Verifiable();

        await sut.GetReferenceTokenAsync("test");

        storeMock.Verify(m => m.GetAsync("test", null));
    }

    [Fact]
    public async Task RemoveReferenceTokenAsync_should_call_store_DeleteAsync()
    {
        CreateSut(out Mock<IAdminStore<ReferenceToken>> storeMock,
            out ReferenceTokenStore sut);

        storeMock.Setup(m => m.DeleteAsync(It.IsAny<string>())).Verifiable();

        storeMock.Setup(m => m.GetAsync(It.IsAny<string>(), It.IsAny<GetRequest>()))
            .ReturnsAsync(new ReferenceToken
            {
                Id = "id"
            })
            .Verifiable();

        await sut.RemoveReferenceTokenAsync("test");

        storeMock.Verify(m => m.GetAsync("test", null));
        storeMock.Verify(m => m.DeleteAsync(It.Is<string>(r => r == "id")));
    }

    [Fact]
    public async Task RemoveReferenceTokensAsync_should_call_store_DeleteAsync()
    {
        CreateSut(out Mock<IAdminStore<ReferenceToken>> storeMock,
            out ReferenceTokenStore sut);

        storeMock.Setup(m => m.DeleteAsync(It.IsAny<string>())).Verifiable();

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<ReferenceToken>
            {
                Count = 1,
                Items =
                [
                    new ReferenceToken
                    {
                        Id = "id"
                    }
                ]
            })
            .Verifiable();

        await sut.RemoveReferenceTokensAsync("test", "test");

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p => p.Filter == "UserId eq 'test' and ClientId eq 'test'")));
        storeMock.Verify(m => m.DeleteAsync(It.Is<string>(r => r == "id")));
    }

    [Fact]
    public async Task StoreReferenceTokenAsync_should_call_store_CreateAsync()
    {
        CreateSut(out Mock<IAdminStore<ReferenceToken>> storeMock,
            out ReferenceTokenStore sut);

        storeMock.Setup(m => m.CreateAsync(It.IsAny<ReferenceToken>()))
            .ReturnsAsync(new ReferenceToken())
            .Verifiable();

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<ReferenceToken>
            {
                Count = 0,
                Items = []
            })
            .Verifiable();

        await sut.StoreReferenceTokenAsync(new ISModels.Token()
        {
            ClientId = "test"
        });

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p => p.Filter == "UserId eq '' and ClientId eq 'test'")));
        storeMock.Verify(m => m.CreateAsync(It.IsAny<ReferenceToken>()));

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<ReferenceToken>
            {
                Count = 1,
                Items =
                [
                    new ReferenceToken()
                ]
            })
            .Verifiable();
        storeMock.Setup(m => m.UpdateAsync(It.IsAny<ReferenceToken>()))
            .ReturnsAsync(new ReferenceToken())
            .Verifiable();

        await sut.StoreReferenceTokenAsync(new ISModels.Token()
        {
            ClientId = "test"
        });

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p => p.Filter == "UserId eq '' and ClientId eq 'test'")));
        storeMock.Verify(m => m.UpdateAsync(It.IsAny<ReferenceToken>()));
    }

    private static void CreateSut(out Mock<IAdminStore<ReferenceToken>> storeMock,
        out ReferenceTokenStore sut)
    {
        storeMock = new Mock<IAdminStore<ReferenceToken>>();
        var serializerMock = new Mock<IPersistentGrantSerializer>();
        sut = new ReferenceTokenStore(storeMock.Object, serializerMock.Object);
    }
}